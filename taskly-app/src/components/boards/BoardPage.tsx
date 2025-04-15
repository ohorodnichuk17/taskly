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
import { SelectDeadlineComponent } from "../general/SelectDeadlineComponent";
import { done_card_list } from "../../constants/constants";



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
    const descriptionTextAreaRef = useRef<HTMLTextAreaElement | null>(null);
    const editDescriptionButtonRef = useRef<HTMLButtonElement | null>(null);

    const [boardPageOverflowX, setBoardPageOverflowX] = useState<"auto" | "scroll">("auto");
    const [dragenCard, setDragenCard] = useState<{
        cardId: string,
        fromCardListId: string,
    } | null>(null);
    const [cardLists, setCardLists] = useState<ICardListItem[] | null>(null);
    const [dropDownCardId, setDropDownCardId] = useState<string | null>(null);
    //const [changeDeadline, setChangeDeadline] = useState<string | null>(null);
    const [changeCardProps, setChangeCardProps] = useState<
        {
            prop: ("description" | "deadline"),
            cardId: string
        } | null>(null);



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
            toCardListId: string,
            isCompleated: boolean
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
        conn.current.on("ChangeCardInformation", (model: {
            cardListId: string,
            cardId: string,
            userId: string,
            changeProps: {
                description: string | null | undefined,
                deadline: Date | null | undefined
            }
        }) => {
            if (userId != model.userId) {
                changeCard({
                    cardListId: model.cardListId,
                    cardId: model.cardId,
                    description: model.changeProps.description,
                    deadline: model.changeProps.deadline
                });
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
        toCardListId: string,
        isCompleated: boolean
    }) => {
        if (cardListRef.current !== null) {
            let dragenCardItem: ICard | null = null;

            cardListRef.current.forEach(item => {
                if (item.cards && item.id === model.fromCardListId) {
                    dragenCardItem = item.cards.find(card => card.id === model.cardId) || null;
                }
            })



            let updated = cardListRef.current.map(item => ({
                ...item,
                cards: item.cards && item.id === model.fromCardListId ?
                    findAndRemoveItemFromArray(el => el.id === model.cardId, item.cards) :
                    item.cards && item.id === model.toCardListId ?
                        addItemToArrayFromAnotherArray(dragenCardItem, item.cards) :
                        item.cards


            })).map(item => ({
                ...item,
                cards: item.cards && item.cards.map((card): ICard => ({
                    ...card,
                    isCompleated: card.id === model.cardId ?
                        model.isCompleated :
                        (card.isCompleated === true ? true : false)
                }))
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
    const changeCard = (model: {
        cardListId: string,
        cardId: string,
        description: string | null | undefined,
        deadline: Date | null | undefined
    }) => {
        if (cardListRef.current !== null) {
            const updated = cardListRef.current.map((item) => ({
                ...item,
                cards: item.id === model.cardListId ? (
                    item.cards ? item.cards.map((card) => {
                        if (card.id === model.cardId) {
                            return {
                                ...card,
                                description: model.description ? model.description : card.description,
                                endTime: model.deadline ? model.deadline : card.endTime,
                            }
                        }
                        return card;
                    }) :
                        item.cards)
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
            {/*<SelectDeadlineComponent />*/}
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
                                    toCardListId: element.id,
                                    isCompleated: element.title === done_card_list
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
                                if (element_card.userId === null ||
                                    (element_card.userId !== null && element_card.userId === userId)) {
                                    setDragenCard({
                                        cardId: element_card.id,
                                        fromCardListId: element.id
                                    });
                                }

                            }}
                                draggable={
                                    element_card.userId === null ||
                                    (element_card.userId !== null && element_card.userId === userId)}
                            >
                                <div className="card-task">
                                    {changeCardProps &&
                                        changeCardProps.prop === "description" &&
                                        changeCardProps.cardId === element_card.id ?
                                        <textarea
                                            name="card_description"
                                            id=""
                                            defaultValue={element_card.description}
                                            ref={(ref) => {
                                                descriptionTextAreaRef.current = ref;
                                                descriptionTextAreaRef.current?.focus();
                                                descriptionTextAreaRef.current?.setSelectionRange(
                                                    descriptionTextAreaRef.current.value.length,
                                                    descriptionTextAreaRef.current.value.length);
                                            }}
                                            onBlur={async () => {
                                                changeCard({
                                                    cardListId: element.id,
                                                    cardId: element_card.id,
                                                    description: descriptionTextAreaRef.current?.value,
                                                    deadline: null
                                                });
                                                if (conn.current)
                                                    await conn.current.send("ChangeCardInformation",
                                                        {
                                                            changeProps: {
                                                                description: descriptionTextAreaRef.current?.value,
                                                                deadline: null
                                                            },
                                                            boardId: boardId,
                                                            cardListId: element.id,
                                                            cardId: element_card.id,
                                                            userId: userId
                                                        }
                                                    )
                                                descriptionTextAreaRef.current = null;
                                                setChangeCardProps(null);
                                            }}
                                            onChange={() => {
                                                descriptionTextAreaRef.current!.style.height = `${descriptionTextAreaRef.current!.scrollHeight}px`;
                                            }}></textarea>
                                        :
                                        <p>{element_card.description}</p>}

                                </div>

                                <div className="card-information">
                                    <div className="card-deadline"
                                        style={{
                                            backgroundColor: element_card.isCompleated === true ? "deepskyblue" : (Date.parse(element_card.endTime.toString()) < Date.now()) ? "red" : "green",
                                            cursor: element_card.userId === userId ? "pointer" : "default"
                                        }}
                                        onClick={() => {
                                            if (element_card.userId === userId && element_card.isCompleated === false) {
                                                if (changeCardProps && changeCardProps.prop === "deadline" && changeCardProps.cardId === element_card.id) return;
                                                setChangeCardProps({
                                                    prop: "deadline",
                                                    cardId: element_card.id
                                                });
                                            }

                                        }}
                                    >
                                        {changeCardProps &&
                                            changeCardProps.prop === "deadline" &&
                                            changeCardProps.cardId === element_card.id ?
                                            <input
                                                type="date"
                                                ref={(ref) => {
                                                    ref?.focus();
                                                }}
                                                min={format(element_card.startTime, "yyyy-MM-dd")}
                                                max={format((() => {
                                                    const currentYear = new Date();
                                                    currentYear.setFullYear(currentYear.getFullYear() + 1);
                                                    return currentYear;
                                                })(), "yyyy-MM-dd")}
                                                defaultValue={format(element_card.endTime, "yyyy-MM-dd")}
                                                onBlur={async (e) => {
                                                    if (e.target.value > format(element_card.startTime, "yyyy-MM-dd") && e.target.value < format((() => {
                                                        const currentYear = new Date();
                                                        currentYear.setFullYear(currentYear.getFullYear() + 1);
                                                        return currentYear;
                                                    })(), "yyyy-MM-dd")) {
                                                        if (conn.current)
                                                            await conn.current.send("ChangeCardInformation", {
                                                                changeProps: {
                                                                    description: null,
                                                                    deadline: new Date(e.target.value)
                                                                },
                                                                boardId: boardId,
                                                                cardListId: element.id,
                                                                cardId: element_card.id,
                                                                userId: userId
                                                            })

                                                        changeCard({
                                                            cardListId: element.id,
                                                            cardId: element_card.id,
                                                            description: null,
                                                            deadline: new Date(e.target.value)
                                                        })
                                                    }

                                                    setChangeCardProps(null);
                                                }}
                                            /> :
                                            <p>{element_card.isCompleated === true ? "Compleated" : `Deadline : ${format(element_card.endTime, "yyyy-MM-dd")}`}</p>
                                        }

                                    </div>
                                    {element_card.userId === userId ?
                                        <div className="card-settings-buttons">
                                            <button
                                                className={changeCardProps &&
                                                    changeCardProps.prop === "description" &&
                                                    changeCardProps.cardId === element_card.id ?
                                                    "active" :
                                                    "inactive"}
                                                onClick={() => {
                                                    setChangeCardProps({
                                                        prop: "description",
                                                        cardId: element_card.id
                                                    })
                                                    editDescriptionButtonRef.current?.focus();
                                                }}
                                            >
                                                <img src={edit_icon} alt="Edit card" />
                                            </button>
                                            <button
                                                onClick={async () => {
                                                    if (conn.current)
                                                        await conn.current.send("RemoveCardFromCardList", {
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
                                        :
                                        <div className="creator-of-card">
                                            <img
                                                className="creator-of-card"
                                                src={`${baseUrl}/images/avatars/${element_card.userAvatar}.png`}
                                                alt=""
                                                onMouseEnter={(e) => {
                                                    setDropDownCardId(element_card.id);
                                                }}
                                                onMouseLeave={() => {
                                                    setDropDownCardId(null);
                                                }}
                                            />
                                            {dropDownCardId !== null && dropDownCardId === element_card.id &&
                                                <div
                                                    className="dropdown-user-name"
                                                >
                                                    {element_card.userName}
                                                </div>
                                            }
                                        </div>


                                    }

                                </div>
                            </div>
                        ))}
                    </div>

                </div>
            ))}
        </div>
    </div >
}