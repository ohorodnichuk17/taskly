import { useEffect, useLayoutEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom"
import '../../styles/board/board-page-style.scss';
import { useAppDispatch, useRootState } from "../../redux/hooks";
import { getCardsListsByBoardIdAsync } from "../../redux/actions/boardsAction";
import { format } from "date-fns";
import trash_can_icon from '../../../public/icon/trash_can_icon.png';
import edit_icon from '../../../public/icon/edit_icon.png';
import { ICard, ICardListItem } from "../../interfaces/boardInterface";
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import { baseUrl } from "../../axios/baseUrl";



const addItemToArrayFromAnotherArray = (item: any, array: any[]) => {

    let newArr = [...array, item];

    return newArr;
}
const findAndRemoveItemFromArray = (condition: (c: any) => boolean, array: any[]) => {
    let item = array.find(condition);
    if (item) {
        let index = array.indexOf(item);
        if (index !== -1) {
            return [...array.slice(0, index), ...array.slice(index + 1)];
        }
    }
    return array;
}

export const BoardPage = () => {

    const conn = useRef<HubConnection | null>(null);

    /*const conn = new HubConnectionBuilder()
        .withUrl(`${baseUrl}/board`)
        .configureLogging(LogLevel.Information)
        .build();*/

    const { boardId } = useParams();

    const cardList = useRootState(s => s.board.cardList);
    const userId = useRootState(s => s.authenticate.userProfile?.id);
    const dispatch = useAppDispatch();


    const boardPageRef = useRef<HTMLDivElement | null>(null);
    const cardsRef = useRef<{
        element: (HTMLDivElement | null)
        id: string
    }[]>([]);
    const cardListRef = useRef<ICardListItem[] | null>(null);

    const [boardPageOverflowX, setBoardPageOverflowX] = useState<"auto" | "scroll">("auto");
    const [dragenCard, setDragenCard] = useState<{
        cardId: string,
        fromCardListId: string,
    } | null>(null);
    const [cardLists, setCardLists] = useState<ICardListItem[] | null>(null);
    const [settingsOpenedId, setSettingsOpenedId] = useState<string | null>(null);



    const getCardList = async () => {
        if (boardId != null)
            await dispatch(getCardsListsByBoardIdAsync(boardId))
    }

    useEffect(() => {
        if (cardList)
            setCardLists(cardList);
    }, [cardList])
    useLayoutEffect(() => {
        cardListRef.current = cardLists;
        if (cardsRef.current) {
            cardsRef.current.forEach((el) => {
                if (el.element) {
                    if (el.element.scrollHeight > el.element.offsetHeight) {
                        el.element.style.overflowY = "scroll";
                        el.element.scrollTo(0, el.element.scrollHeight);
                    }
                    else {
                        el.element.style.overflowY = "auto";
                        el.element.scrollTo(0, 0);
                    }
                }
            });
        }
    }, [cardLists])
    useEffect(() => {
        if (boardPageRef.current) {
            setBoardPageOverflowX(boardPageRef.current.offsetWidth < boardPageRef.current.scrollWidth ?
                "scroll" :
                "auto"

            )
        }
    }, [boardPageRef.current])


    useLayoutEffect(() => {
        getCardList();
        startConnection();
    }, [])



    const startConnection = async () => {

        conn.current = new HubConnectionBuilder()
            .withUrl(`${baseUrl}/board`)
            .configureLogging(LogLevel.Information)
            .build();

        conn.current.on("ConnectToTeamBoard", (mess) => {
            console.log(mess)
        });
        conn.current.on("TransferCardToAnotherCardList", (model: {
            userId: string,
            cardId: string,
            fromCardListId: string,
            toCardListId: string
        }) => {
            if (userId != model.userId) {
                transferCardToAnotherCardList(model);
            }

        })
        conn.current.on("RemoveCardFromCardList", (model: {
            cardListId: string,
            cardId: string,
            userId: string
        }) => {
            if (userId != model.userId) {
                removeCardFromCardList(model);
            }
        })

        await conn.current.start();
        await conn.current.invoke("ConnectToTeamBoard", { userId, boardId });
        //await conn.stop()
    }

    const endConnection = async () => {
        if (conn.current != null) {
            await conn.current.invoke("DisconnectFromTeamBoard", { userId, boardId });
            await conn.current.stop();
        }

    }

    useEffect(() => {
        return (() => {

            endConnection();
        });
    }, [])


    const transferCardToAnotherCardList = (model: {
        cardId: string,
        fromCardListId: string,
        toCardListId: string
    }) => {
        if (cardListRef.current !== null) {
            let dragenCardItem: ICard | null = null;

            cardListRef.current.forEach(item => {
                if (item.cards && item.id === model.fromCardListId) {
                    dragenCardItem = item.cards.find(card => card.id === model.cardId) || null;
                }
            })

            const updated = cardListRef.current.map(item => ({
                ...item,
                cards: item.cards && item.id === model.fromCardListId ?
                    findAndRemoveItemFromArray(el => el.id === model.cardId, item.cards) :
                    item.cards && item.id === model.toCardListId ?
                        addItemToArrayFromAnotherArray(dragenCardItem, item.cards) :
                        item.cards


            }));

            setCardLists(updated);

        }

    }
    const removeCardFromCardList = (model: {
        cardListId: string,
        cardId: string,
        userId: string
    }) => {
        if (cardListRef.current !== null) {
            const updated = cardListRef.current.map((item) => ({
                ...item,
                cards: item.cards && item.cards.find((card) => card.id === model.cardId) ?
                    findAndRemoveItemFromArray((card) => card.id === model.cardId, item.cards)
                    :
                    item.cards
            }));

            setCardLists(updated);
        }
    }
    return <div className="board-page-container"
        ref={(ref) => {
            boardPageRef.current = ref;
        }}
        style={{ overflowX: boardPageOverflowX }}
    >
        <div className="card-list-container">
            {cardLists && cardLists.map((element) => (
                <div className="card-list-item" key={element.id}>
                    <h2>{element.title}</h2>
                    <div className="card-list-cards"
                        ref={(ref) => {
                            cardsRef.current.push({
                                element: ref,
                                id: element.id
                            });
                        }}
                        onDrop={async (e) => {
                            e.preventDefault();
                            if (dragenCard && dragenCard.fromCardListId !== element.id) {
                                transferCardToAnotherCardList({
                                    cardId: dragenCard.cardId,
                                    fromCardListId: dragenCard.fromCardListId,
                                    toCardListId: element.id
                                })

                                if (conn.current)
                                    await conn.current.invoke("TransferCardToAnotherCardList", {
                                        userId: userId,
                                        cardId: dragenCard.cardId,
                                        fromCardListId: dragenCard.fromCardListId,
                                        toCardListId: element.id,
                                        boardId: boardId
                                    })


                                setDragenCard(null);
                            }

                        }}
                        onDragOver={(e) => {
                            e.preventDefault();
                        }}
                    >
                        {element.cards && element.cards.map((element_card) => (
                            <div className="card" key={element_card.id} onDragStart={() => {

                                setDragenCard({
                                    cardId: element_card.id,
                                    fromCardListId: element.id
                                });
                            }}
                                draggable>
                                <p>{element_card.description}</p>
                                <div className="card-information">
                                    <div className="card-deadline"
                                        style={{
                                            backgroundColor: (Date.parse(element_card.endTime.toString()) < Date.now()) ? "red" : "green"
                                        }}
                                    >
                                        Deadline : {format(element_card.endTime, "yyyy-MM-dd")}
                                    </div>
                                    <div className="card-settings-buttons">
                                        <button>
                                            <img src={edit_icon} alt="Edit card" />
                                        </button>
                                        <button
                                            onClick={async () => {
                                                if (conn.current)
                                                    await conn.current.invoke("RemoveCardFromCardList", {
                                                        boardId: boardId,
                                                        cardListId: element.id,
                                                        cardId: element_card.id,
                                                        userId: userId!
                                                    })
                                                removeCardFromCardList({
                                                    cardListId: element.id,
                                                    cardId: element_card.id,
                                                    userId: userId!
                                                })
                                            }}>
                                            <img src={trash_can_icon} alt="Remove card" />
                                        </button>

                                    </div>
                                </div>
                                {/*settingsOpenedId && settingsOpenedId === element_card.id &&
                                    <div className="settings">
                                        <button>Remove</button>
                                        <button>Edit Task</button>
                                        <button>Edit Deadline</button>
                                    </div>*/}
                            </div>
                        ))}
                    </div>

                </div>
            ))}
        </div>
    </div >
}