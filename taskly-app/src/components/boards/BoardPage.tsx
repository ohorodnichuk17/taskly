import { useEffect, useLayoutEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom"
import '../../styles/board/board-page-style.scss';
import { useAppDispatch, useRootState } from "../../redux/hooks";
import { getCardsListsByBoardIdAsync } from "../../redux/actions/boardsAction";
import { format } from "date-fns";
import trash_can_icon from '../../../public/icon/trash_can_icon.png';
import edit_icon from '../../../public/icon/edit_icon.png';
import leave_card_icon from '../../../public/icon/leave_card_icon.png';
import take_card_icon from '../../../public/icon/take_card_icon.png';
import ai_cards_icon from '../../../public/icon/ai_cards_icon.png';
import create_custom_card_icon from '../../../public/icon/create_custom_card_icon.png';
import { ICard, ICardListItem } from "../../interfaces/boardInterface";
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import { baseUrl } from "../../axios/baseUrl";
import { SelectDeadlineComponent } from "../general/SelectDeadlineComponent";
import { done_card_list, todo_card_list } from "../../constants/constants";
import { TemplateOfCard } from "../general/TemplateOfCard";
import { TaskTextArea } from "../general/TaskTextArea";
import { CardType } from "../../validation_types/types";
import { createCardAsync } from "../../redux/actions/cardsActions";



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
    const user = useRootState(s => s.authenticate.userProfile);
    const dispatch = useAppDispatch();


    const boardPageRef = useRef<HTMLDivElement | null>(null);
    const cardsRef = useRef<{
        element: (HTMLDivElement | null)
        id: string
    }[]>([]);
    const cardListRef = useRef<ICardListItem[] | null>(null);
    //const descriptionTextAreaRef = useRef<HTMLTextAreaElement | null>(null);
    const editDescriptionButtonRef = useRef<HTMLButtonElement | null>(null);

    const [boardPageOverflowX, setBoardPageOverflowX] = useState<"auto" | "scroll">("auto");
    const [dragenCard, setDragenCard] = useState<{
        cardId: string,
        fromCardListId: string,
    } | null>(null);
    const [cardLists, setCardLists] = useState<ICardListItem[] | null>(null);
    const [dropDownCardId, setDropDownCardId] = useState<string | null>(null);
    const [changeCardProps, setChangeCardProps] = useState<
        {
            prop: ("description" | "deadline"),
            cardId: string
        } | null>(null);
    const [creatorOfCardPosition, setCreatorOfCardPosition] = useState<DOMRect | null>(null);
    const [openedCustomCardCreate, setOpenedCustomCardCreate] = useState<boolean>(false);
    //const [cardDescription, setCardDescription] = useState<string | null>(null);
    const cardDescription = useRef<string | null>(null);


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
        });
        conn.current.on("LeaveCard", (model: {
            cardListId: string,
            cardId: string,
            userId: string
        }) => {
            if (userId != model.userId) {
                leaveCard({
                    cardListId: model.cardListId,
                    cardId: model.cardId
                });
            }
        });
        conn.current.on("TakeCard", (model: {
            cardListId: string,
            cardId: string,
            userId: string,
            userName: string,
            userAvatar: string
        }) => {
            if (userId != model.userId) {
                takeCard({
                    cardListId: model.cardListId,
                    cardId: model.cardId,
                    userId: model.userId,
                    userName: model.userName,
                    userAvatar: model.userAvatar
                });
            }
        });
        conn.current.on("AddNewCard", (model: {
            cardListId: string,
            cardId: string,
            task: string,
            deadline: Date,
            userId: string | null,
            userAvatar: string | null,
            userName: string | null
        }) => {
            addNewCard(model);
        });

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



            const updated = cardListRef.current.map(item => ({
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
                cards: item.id === model.cardListId && item.cards ?
                    item.cards.map((card) => {
                        if (card.id === model.cardId) {
                            return {
                                ...card,
                                description: model.description ? model.description : card.description,
                                endTime: model.deadline ? model.deadline : card.endTime,
                            }
                        }
                        return card;
                    })
                    : item.cards

            }));

            setCardLists(updated);
        }
    }
    const leaveCard = (model: {
        cardListId: string,
        cardId: string
    }) => {
        if (cardListRef.current !== null) {
            const update = cardListRef.current.map((item) => ({
                ...item,
                cards: item.id === model.cardListId && item.cards ? (
                    item.cards.map((card) => {
                        if (card.id === model.cardId) {
                            return {
                                ...card,
                                userId: null,
                                userName: null,
                                userAvatar: null
                            }
                        }
                        return card;
                    })
                ) : item.cards
            }));

            setCardLists(update);
        }
    }
    const takeCard = (model: {
        cardListId: string,
        cardId: string,
        userId: string,
        userName: string,
        userAvatar: string
    }) => {
        if (cardListRef.current !== null) {
            const updated = cardListRef.current.map((item) => ({
                ...item,
                cards: item.id === model.cardListId && item.cards ?
                    item.cards.map((card) => {
                        if (card.id === model.cardId) {
                            return {
                                ...card,
                                userId: model.userId,
                                userName: model.userName,
                                userAvatar: model.userAvatar
                            }
                        }
                        return card;
                    })
                    : item.cards
            }))

            setCardLists(updated);
        }
    }
    const addNewCard = (model: {
        cardListId: string,
        cardId: string,
        task: string,
        deadline: Date,
        userId: string | null,
        userAvatar: string | null,
        userName: string | null
    }) => {
        if (cardListRef.current !== null) {

            const update = cardListRef.current.map((item) => ({
                ...item,
                cards: item.id === model.cardListId && item.cards ? [...item.cards, {
                    id: model.cardId,
                    title: null,
                    description: model.task,
                    attachmentUrl: null,
                    userId: model.userId,
                    isCompleated: false,
                    userAvatar: model.userAvatar,
                    userName: model.userName,
                    status: todo_card_list,
                    startTime: new Date(),
                    endTime: model.deadline,
                    comments: null
                }] : item.cards
            }));

            setCardLists(update);
        }
    };



    const handleSubmit = async (request: CardType) => {
        const model = {
            cardListId: cardLists!.find(cardList => cardList.title === todo_card_list)!.id,
            task: request.task,
            deadline: request.deadline,
            userId: request.isPublicCard === true ? null : userId!
        }
        console.log("MODEL", model)
        const response = await dispatch(createCardAsync(model));

        if (createCardAsync.fulfilled.match(response)) {
            console.log("User", user);
            if (conn.current) {
                await conn.current.send("AddNewCard", {
                    boardId: boardId,
                    cardListId: cardLists!.find(cardList => cardList.title === todo_card_list)!.id,
                    cardId: response.payload,
                    task: request.task,
                    deadline: request.deadline,
                    userId: request.isPublicCard === true ? null : userId!,
                    userAvatar: request.isPublicCard === true ? null : user?.avatarName,
                    userName: request.isPublicCard === true ? null : user?.email
                });
            }
            setOpenedCustomCardCreate(false);
        }
    }

    return <div className="board-page-container"
        ref={(ref) => {
            boardPageRef.current = ref;
        }}
        style={{ overflowX: boardPageOverflowX }}
        onScroll={() => {
            if (creatorOfCardPosition !== null) {
                setCreatorOfCardPosition(null);
            }
        }}
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
                                        boardId: boardId,
                                        isCompleated: element.title === done_card_list
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
                                    (element_card.userId === null ||
                                        (element_card.userId !== null && element_card.userId === userId)) &&
                                    element_card.isCompleated !== true}
                            >
                                <div className="card-task">
                                    {changeCardProps &&
                                        changeCardProps.prop === "description" &&
                                        changeCardProps.cardId === element_card.id ?
                                        <TaskTextArea
                                            defaultValue={element_card.description}
                                            value={cardDescription}
                                            maxLength={300}
                                            onBlur={async () => {
                                                changeCard({
                                                    cardListId: element.id,
                                                    cardId: element_card.id,
                                                    description: cardDescription.current,
                                                    deadline: null
                                                });
                                                if (conn.current)
                                                    await conn.current.send("ChangeCardInformation",
                                                        {
                                                            changeProps: {
                                                                description: cardDescription.current,
                                                                deadline: null
                                                            },
                                                            boardId: boardId,
                                                            cardListId: element.id,
                                                            cardId: element_card.id,
                                                            userId: userId
                                                        }
                                                    )

                                                cardDescription.current = null
                                                setChangeCardProps(null);
                                            }}
                                        />

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
                                    {element_card.userId !== null && element_card.userId === userId ?
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
                                            <button
                                                onClick={async () => {
                                                    if (conn.current) {
                                                        await conn.current.send("LeaveCard", {
                                                            boardId: boardId,
                                                            cardListId: element.id,
                                                            cardId: element_card.id,
                                                            userId: userId!
                                                        });
                                                        leaveCard({
                                                            cardListId: element.id,
                                                            cardId: element_card.id
                                                        })
                                                    }
                                                }}
                                            >
                                                <img src={leave_card_icon} alt="Leave card" />
                                            </button>

                                        </div>
                                        : (element_card.userId !== null ?
                                            <div className="creator-of-card">
                                                <img
                                                    className="creator-of-card"
                                                    src={`${baseUrl}/images/avatars/${element_card.userAvatar}.png`}
                                                    alt=""

                                                    onMouseEnter={(e) => {
                                                        setDropDownCardId(element_card.id);
                                                        setCreatorOfCardPosition(e.currentTarget.getBoundingClientRect()); // Дістає позицію елемента відносно вікна
                                                    }}
                                                    onMouseLeave={() => {
                                                        setDropDownCardId(null);
                                                        setCreatorOfCardPosition(null);
                                                    }}

                                                />
                                                {dropDownCardId !== null &&
                                                    dropDownCardId === element_card.id &&
                                                    creatorOfCardPosition !== null &&
                                                    <div
                                                        className="dropdown-user-name"
                                                        style={{
                                                            position: "fixed",
                                                            top: `${creatorOfCardPosition.top + 50}px`,
                                                            left: `${creatorOfCardPosition.left}px`,
                                                        }}

                                                    >
                                                        {element_card.userName}
                                                    </div>
                                                }
                                            </div> : <div className="take-card">
                                                <button onClick={async () => {
                                                    if (conn.current !== null) {
                                                        await conn.current.send("TakeCard", {
                                                            boardId: boardId,
                                                            cardListId: element.id,
                                                            cardId: element_card.id,
                                                            userId: userId,
                                                            userName: user!.email,
                                                            userAvatar: user!.avatarName
                                                        });

                                                        takeCard({
                                                            cardListId: element.id,
                                                            cardId: element_card.id,
                                                            userId: userId!,
                                                            userName: user!.email,
                                                            userAvatar: user!.avatarName
                                                        })
                                                    }
                                                }}>
                                                    <img src={take_card_icon} alt="Take card" />
                                                </button>
                                            </div>)
                                    }

                                </div>
                            </div>
                        ))}
                        {element.title === todo_card_list &&
                            (openedCustomCardCreate === false ? <div className="create-card-buttons">
                                <button
                                    onClick={() => {
                                        setOpenedCustomCardCreate(true);
                                    }}>
                                    <p>Create custom card</p>
                                    <img src={create_custom_card_icon} alt="Create custom card" />
                                </button>
                                <button>
                                    <p>Create card with AI</p>
                                    <img src={ai_cards_icon} alt="Create card with AI" />
                                </button>
                            </div> :
                                <TemplateOfCard
                                    handleSubmit={handleSubmit}
                                    onClose={() => {
                                        setOpenedCustomCardCreate(false);
                                    }} />)

                        }
                    </div>

                </div>
            ))}
        </div>
    </div >
}