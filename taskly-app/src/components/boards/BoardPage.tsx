import { useEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom"
import '../../styles/board/board-page-style.scss';
import { useAppDispatch, useRootState } from "../../redux/hooks";
import { getCardsListsByBoardIdAsync } from "../../redux/actions/boardsAction";
import { format } from "date-fns";
import setting_icon from '../../../public/icon/setting_icon.png';
import { ICard, ICardListItem } from "../../interfaces/boardInterface";
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import { baseUrl } from "../../axios/baseUrl";



const addItemToArrayFromAnotherArray = (item: any, array: any[]) => {

    let newArr = [...array, item];

    return newArr;
}
const findAndRemoveItemFromArray = (condition: (c: any) => boolean, array: any[]) => {
    console.log("remove card - ");
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

    const conn = new HubConnectionBuilder()
        .withUrl(`${baseUrl}/board`)
        .configureLogging(LogLevel.Information)
        .build();

    const { boardId } = useParams();

    const cardList = useRootState(s => s.board.cardList);
    const userId = useRootState(s => s.authenticate.userProfile?.id);
    const dispatch = useAppDispatch();


    const boardPageRef = useRef<HTMLDivElement | null>(null);
    const cardsRef = useRef<{
        element: (HTMLDivElement | null)
        id: string
    }[]>([]);

    const [boardPageOverflowX, setBoardPageOverflowX] = useState<"auto" | "scroll">("auto");
    const [cardsOverflowY, setCardsOverflowY] = useState<{
        id: string,
        scroll: "auto" | "scroll"
    }[]>([]);
    const [dragenCard, setDragenCard] = useState<{
        cardId: string,
        fromCardListId: string,
    } | null>(null);
    const [cardLists, setCardLists] = useState<ICardListItem[] | null>(null);


    const getCardList = async () => {
        if (boardId != null)
            await dispatch(getCardsListsByBoardIdAsync(boardId))
    }

    useEffect(() => {
        if (cardList)
            setCardLists(cardList);
    }, [cardList])
    useEffect(() => {
        if (boardPageRef.current) {
            setBoardPageOverflowX(boardPageRef.current.offsetWidth < boardPageRef.current.scrollWidth ?
                "scroll" :
                "auto"

            )
        }
    }, [boardPageRef.current])

    useEffect(() => {
        if (cardsRef.current) {
            cardsRef.current.forEach((element) => {
                if (element.element && element.element.clientHeight > element.element.offsetHeight) {
                    const scrollableCard = cardsOverflowY.find((card) => card.id === element.id);
                    if (scrollableCard) {
                        scrollableCard.scroll = "scroll";
                    }
                }
            })

        }
    }, [cardsRef.current])

    useEffect(() => {
        getCardList();
        startConnection();
    }, [])

    useEffect(() => {
        console.log("cardLists змінився - ", cardLists)
    }, [cardLists])

    const startConnection = async () => {


        conn.on("ConnectToTeamBoard", (mess) => {
            console.log(mess)
        });
        conn.on("TransferCardToAnotherCardList", (model: {
            userId: string,
            cardId: string,
            fromCardListId: string,
            toCardListId: string
        }) => {
            console.log("Model - ", model)
            if (cardLists === null) {
                console.error("cardLists є null перед оновленням!");
            }
            if (userId != model.userId) {
                transferCardToAnotherCardList(model);
            }

        })
        conn.on("DisconnectFromTeamBoard", (mess) => {
            console.log(mess);
        })

        await conn.start();
        await conn.invoke("ConnectToTeamBoard", { userId, boardId });
        //await conn.stop()
    }

    const endConnection = async () => {
        await conn.invoke("DisconnectFromTeamBoard", { userId, boardId });
        await conn.stop();
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
        if (cardLists !== null) {
            console.log("TRANSFER")
            let dragenCardItem: ICard | null = null;

            cardLists.forEach(item => {
                if (item.cards && item.id === model.fromCardListId) {
                    dragenCardItem = item.cards.find(card => card.id === model.cardId) || null;
                }
            })
            setCardLists(prev => prev ? prev.map(item => ({
                ...item,
                cards: item.cards && item.id === model.fromCardListId ?
                    findAndRemoveItemFromArray(el => el.id === model.cardId, item.cards) :
                    item.cards && item.id === model.toCardListId ?
                        addItemToArrayFromAnotherArray(dragenCardItem, item.cards) :
                        item.cards


            })) : []);

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

                            cardsOverflowY.push({
                                id: element.id,
                                scroll: "auto"
                            })
                        }}
                        style={{
                            overflowY:
                                cardsOverflowY.find((card) => card.id === element.id) ?
                                    cardsOverflowY.find((card) => card.id === element.id)?.scroll :
                                    "auto"
                        }}
                        onDrop={async (e) => {
                            e.preventDefault();
                            if (dragenCard && dragenCard.fromCardListId !== element.id) {
                                transferCardToAnotherCardList({
                                    cardId: dragenCard.cardId,
                                    fromCardListId: dragenCard.fromCardListId,
                                    toCardListId: element.id
                                })
                                if (conn.state !== HubConnectionState.Connected) {
                                    conn.start()
                                        .then(async () => {
                                            await conn.invoke("TransferCardToAnotherCardList", {
                                                userId: userId,
                                                cardId: dragenCard.cardId,
                                                fromCardListId: dragenCard.fromCardListId,
                                                toCardListId: element.id,
                                                boardId: boardId
                                            })
                                        })
                                }
                                else {
                                    await conn.invoke("TransferCardToAnotherCardList", {
                                        userId: userId,
                                        cardId: dragenCard.cardId,
                                        fromCardListId: dragenCard.fromCardListId,
                                        toCardListId: element.id,
                                        boardId: boardId
                                    })
                                }


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
                                <div>
                                    <div className="card-deadline"
                                        style={{
                                            backgroundColor: (Date.parse(element_card.endTime.toString()) < Date.now()) ? "red" : "green"
                                        }}
                                    >
                                        Deadline : {format(element_card.endTime, "yyyy-MM-dd")}
                                    </div>
                                    <button>
                                        <img src={setting_icon} alt="Settings" />
                                    </button>
                                </div>

                            </div>
                        ))}
                    </div>

                </div>
            ))}
        </div>
    </div >
}