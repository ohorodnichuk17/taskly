import { useEffect, useLayoutEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom"
import '../../styles/board/board-page-style.scss';
import { useAppDispatch, useRootState } from "../../redux/hooks";
import { addMemberToBoardAsync, getCardsListsByBoardIdAsync, getMembersOfBoardAsync, leaveBoardAsync, removeMemberFromBoardAsync } from "../../redux/actions/boardsAction";
import { format } from "date-fns";
import trash_can_icon from '../../../public/icon/trash_can_icon.png';
import edit_icon from '../../../public/icon/edit_icon.png';
import leave_card_icon from '../../../public/icon/leave_card_icon.png';
import take_card_icon from '../../../public/icon/take_card_icon.png';
import ai_cards_icon from '../../../public/icon/ai_cards_icon.png';
import create_custom_card_icon from '../../../public/icon/create_custom_card_icon.png';
import members_purple_icon from '../../../public/icon/person_purple_icon.png';
import members_white_icon from '../../../public/icon/person_icon.png';
import leave_board_white_icon from '../../../public/icon/levae_board_white_icon.png';
import leave_board_purple_icon from '../../../public/icon/levae_board_purple_icon.png';
import add_member_white_icon from '../../../public/icon/add_member_white_icon.png';
import add_member_purple_icon from '../../../public/icon/add_member_purple_icon.png';
import { ICard, ICardListItem, IMemberOfBoard } from "../../interfaces/boardInterface";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { baseUrl } from "../../axios/baseUrl";
import { done_card_list, todo_card_list } from "../../constants/constants";
import { TemplateOfCard } from "../general/TemplateOfCard";
import { TaskTextArea } from "../general/TaskTextArea";
import { EmailValidationShema, EmailValidationType, GenerateCardsWithAIType, NewCardType } from "../../validation_types/types";
import { createCardAsync } from "../../redux/actions/cardsActions";
import { GenerateCardsWithAI } from "../general/GenerateCardsWithAI";
import { generateCardsWithAIAsync } from "../../redux/actions/geminiActions";
import { ICreateCardWithAI } from "../../interfaces/cardsInterface";
import { removeCardsOfLeavedUser } from "../../redux/slices/boardSlice";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { InformationAlert } from "../general/InformationAlert";
import { addInformation } from "../../redux/slices/generalSlice";
import { TypeOfInformation } from "../../interfaces/generalInterface";
import { GeneralMode } from "../general/GeneralModal";



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
    const boardPageRef = useRef<HTMLDivElement | null>(null);
    const cardsRef = useRef<{
        element: (HTMLDivElement | null)
        id: string
    }[]>([]);
    const cardListRef = useRef<ICardListItem[] | null>(null);
    const editDescriptionButtonRef = useRef<HTMLButtonElement | null>(null);
    const cardDescription = useRef<string | null>(null);
    const boardMembersRef = useRef<HTMLDivElement | null>(null);
    const membersRef = useRef<IMemberOfBoard[] | null>(null);


    /*const conn = new HubConnectionBuilder()
        .withUrl(`${baseUrl}/board`)
        .configureLogging(LogLevel.Information)
        .build();*/

    const { boardId } = useParams();
    const navigate = useNavigate();

    const information = useRootState((s) => s.general.information);
    const cardList = useRootState(s => s.board.cardList);
    const userId = useRootState(s => s.authenticate.userProfile?.id);
    const user = useRootState(s => s.authenticate.userProfile);
    const cardsOfLeavedUser = useRootState(s => s.board.cardsOfLeavedUser);
    const cardsOfRemovedUser = useRootState(s => s.board.cardsOfRemovedUser);
    const membersOfBoardList = useRootState(s => s.board.membersOfBoard);
    const dispatch = useAppDispatch();

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
    const [openedAICardCreate, setOpenedAICardCreate] = useState<boolean>(false);
    const [openedAddMember, setOpenedAddMember] = useState<boolean>(false);
    const [openedGeneralModal, setOpenedGeneralModal] = useState<boolean>(false);
    const [buttonHovered, setButtonHovered] = useState<"members" | "leave" | "add_member" | null>(null);
    const [membersOfBoard, setMembersOfBoard] = useState<IMemberOfBoard[] | null>(null);
    const [boardMembersOverflowY, setBoardMembersOverflowY] = useState<"auto" | "scroll">("auto");

    const {
        register,
        handleSubmit,
        formState: {
            errors
        },
        setValue
    } = useForm<EmailValidationType>({
        resolver: zodResolver(EmailValidationShema)
    });


    const getCardList = async () => {
        if (boardId != null) {
            await dispatch(getMembersOfBoardAsync(boardId));
            var response = await dispatch(getCardsListsByBoardIdAsync(boardId))

            if (!getCardsListsByBoardIdAsync.fulfilled.match(response)) {
                navigate('/not-found');
            }
        }
        else {
            navigate('/not-found');
        }
    }

    useEffect(() => {
        if (membersOfBoardList !== null) {
            setMembersOfBoard(membersOfBoardList);
        }
    }, [membersOfBoardList])

    useEffect(() => {
        if (membersOfBoard !== null) {
            membersRef.current = membersOfBoard;
        }
    }, [membersOfBoard])

    useEffect(() => {
        if (cardsOfLeavedUser !== null) {
            if (conn.current) {
                conn.current.send("UserHasLeftBoard", {
                    boardId: boardId,
                    cardsId: cardsOfLeavedUser
                });
            }
            dispatch(removeCardsOfLeavedUser());
            navigate("/boards");

        }
    }, [cardsOfLeavedUser])
    useEffect(() => {
        if (cardsOfLeavedUser !== null) {
            if (conn.current) {
                conn.current.send("UserHasBeenRemovedFromBoard", {
                    boardId: boardId,
                    cardsId: cardsOfRemovedUser
                });
            }
            dispatch(removeCardsOfLeavedUser());
            navigate("/boards");

        }
    }, [cardsOfRemovedUser])

    useEffect(() => {
        if (cardList)
            setCardLists(cardList);
    }, [cardList])

    useLayoutEffect(() => {
        if ((!cardListRef.current) || (cardListRef.current && cardLists != null)) {
            if (cardsRef.current) {
                cardsRef.current.forEach((el) => {
                    if (el.element) {
                        if (!cardListRef.current) {
                            scrollElement(el.element);
                        }
                        else {
                            const newCardList = cardLists?.find((cardListItem) => cardListItem.id === el.id);
                            const oldList = cardListRef.current.find((cardListItem) => cardListItem.id === el.id);

                            if (newCardList &&
                                newCardList.cards &&
                                oldList &&
                                oldList.cards &&
                                newCardList.cards.length > oldList.cards.length) {
                                scrollElement(el.element);
                            }

                        }
                    }
                });

            }

        }
        cardListRef.current = cardLists;

    }, [cardLists])

    useEffect(() => {
        if (boardPageRef.current) {
            setBoardPageOverflowX(boardPageRef.current.offsetWidth < boardPageRef.current.scrollWidth ?
                "scroll" :
                "auto"
            )
        }
    }, [boardPageRef.current, boardPageRef.current?.scrollWidth])

    useEffect(() => {
        if ((openedCustomCardCreate === true || openedAICardCreate === true) && cardsRef.current && cardListRef.current) {
            const toDoCardListId = cardListRef.current.find(_cardList => _cardList.title === todo_card_list)!.id || null;
            if (toDoCardListId) {
                const toDoCardList = cardsRef.current.find(_cardList => _cardList.id === toDoCardListId);
                scrollElement(toDoCardList!.element!);
                /*if (toDoCardList!.element!.scrollHeight > toDoCardList!.element!.offsetHeight) {
                    moveToEndOfCardList(toDoCardList!.element!);
                }*/

            }
        }
    }, [openedCustomCardCreate, openedAICardCreate])

    useLayoutEffect(() => {
        getCardList();
        startConnection();
    }, [])

    useEffect(() => {
        return (() => {
            endConnection();
        });
    }, [])
    useEffect(() => {
        if (boardMembersRef.current) {
            setBoardMembersOverflowY(boardMembersRef.current.offsetHeight < boardMembersRef.current.scrollHeight ?
                "scroll" :
                "auto"
            )
        }
    }, [boardMembersRef.current, boardMembersRef.current?.scrollHeight])

    useEffect(() => {
        console.log(errors)
    }, [errors])

    const startConnection = async () => {

        conn.current = new HubConnectionBuilder()
            .withUrl(`${baseUrl}/board`)
            .configureLogging(LogLevel.Information)
            .build();

        conn.current.on("ConnectToTeamBoard", (mess) => {
            console.log(mess)
        });
        conn.current.on("DisconnectFromTeamBoard", (mess) => {
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
        conn.current.on("AddNewCards", (model: {
            cards: {
                cardListId: string,
                cardId: string,
                task: string,
                deadline: Date,
                userId: string | null,
                userAvatar: string | null,
                userName: string | null
            }[]

        }) => {
            addNewCards(model.cards);
        });
        conn.current.on("UserHasLeftBoard", (model: {
            cardsId: string[]
        }) => {
            leaveCards(model);
        });
        conn.current.on("UserHasBeenAddToBoard", (model: {
            addedUserId: string,
            addedUserEmail: string,
            addedUserAvatarName: string,
            userEmailWhoAdd: string
        }) => {
            addMember({
                userId: model.addedUserId,
                email: model.addedUserEmail,
                avatarName: model.addedUserAvatarName
            });
            dispatch(addInformation({
                message: `${model.addedUserEmail} has been added to board by ${model.userEmailWhoAdd}`,
                type: TypeOfInformation.Success
            }));
        });
        conn.current.on("UserHasLeftBoard", (model: {
            cardsId: string[]
        }) => {
            leaveCards(model);
        });
        conn.current.on("UserHasBeenRemovedFromBoard", (model: {
            cardsId: string[],
            removedUserId: string,
            removedUserEmail: string,
            userEmailWhoRemoved: string
        }) => {
            if (userId !== model.removedUserId) {
                dispatch(addInformation({
                    message: `${model.removedUserEmail} has been removed from board by ${model.userEmailWhoRemoved}`,
                    type: TypeOfInformation.Success
                }));
                leaveCards({
                    cardsId: model.cardsId
                });
                removeMemberFromList({
                    email: model.removedUserEmail
                });
            }
            else {
                dispatch(addInformation({
                    message: `You have been removed from board by ${model.userEmailWhoRemoved}`,
                    type: TypeOfInformation.Success
                }));
                navigate('/boards');
            }

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
        cardListId: string | null,
        cardId: string
    }) => {
        if (cardListRef.current !== null) {
            const update = cardListRef.current.map((item) => ({
                ...item,
                cards: ((model.cardListId != null && item.id === model.cardListId) || (model.cardListId == null)) && item.cards ? (
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
    const leaveCards = (model: {
        cardsId: string[]
    }) => {
        if (cardListRef.current !== null) {
            const update = cardListRef.current.map((item) => ({
                ...item,
                cards: item.cards ? (
                    item.cards.map((card) => {
                        if (model.cardsId.findIndex(cardWithoutUser => cardWithoutUser === card.id) !== -1) {
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
    const addNewCards = (model: {
        cardListId: string,
        cardId: string,
        task: string,
        deadline: Date,
        userId: string | null,
        userAvatar: string | null,
        userName: string | null
    }[]) => {
        if (cardListRef.current !== null) {

            const update = cardListRef.current.map((item) => ({
                ...item,
                cards: item.cards ? [...item.cards, ...model
                    .filter(card => item.id === card.cardListId)
                    .map((card) => {
                        return {
                            id: card.cardId,
                            title: null,
                            description: card.task,
                            attachmentUrl: null,
                            userId: card.userId,
                            isCompleated: false,
                            userAvatar: card.userAvatar,
                            userName: card.userName,
                            status: todo_card_list,
                            startTime: new Date(),
                            endTime: card.deadline,
                            comments: null
                        }
                    })] : item.cards
            }));

            setCardLists(update);
        }
    };
    const addMember = (model: {
        userId: string,
        email: string,
        avatarName: string
    }) => {
        if (membersRef.current) {
            const updated = [...membersRef.current, {
                userId: model.userId,
                email: model.email,
                avatarName: model.avatarName
            }];

            setMembersOfBoard(updated);
        }
    }
    const removeMemberFromList = (model: {
        email: string
    }) => {
        if (membersRef.current) {
            console.log(model);
            console.log(membersRef.current);
            const removedUserIndex = membersRef.current.findIndex((member) => member.email === model.email);
            if (removedUserIndex !== -1) {
                const updated = [...membersRef.current.slice(0, removedUserIndex), ...membersRef.current.slice(removedUserIndex + 1, membersRef.current.length)];

                setMembersOfBoard(updated);
            }

        }
    }

    const scrollElement = (cardList: HTMLDivElement) => {
        if (cardList.scrollHeight > cardList.offsetHeight) {
            cardList.style.overflowY = "scroll";
            cardList.scrollTo(0, cardList.scrollHeight);
        }
        else {
            cardList.style.overflowY = "auto";
            cardList.scrollTo(0, 0);
        }
    }
    const createNewCards = () => {
        if (openedCustomCardCreate === false && openedAICardCreate === false) {
            return (
                <div className="create-card-buttons">
                    <button
                        onClick={() => {
                            setOpenedCustomCardCreate(true);
                        }}>
                        <p>Create custom card</p>
                        <img src={create_custom_card_icon} alt="Create custom card" />
                    </button>
                    <button onClick={() => {
                        setOpenedAICardCreate(true);
                    }}>
                        <p>Create card with AI</p>
                        <img src={ai_cards_icon} alt="Create card with AI" />
                    </button>
                </div>
            );
        }
        else if (openedCustomCardCreate === true) {
            return (<TemplateOfCard
                handleSubmit={handleSubmitCreateCustomCard}
                onClose={() => {
                    setOpenedCustomCardCreate(false);
                }} />);
        }
        else {
            return (<GenerateCardsWithAI
                handleSubmit={handleSubmitGenerateCardsWithAI}
                onClose={() => {
                    setOpenedAICardCreate(false);
                }}
            />);
        }
    }
    const leaveBoard = async () => {
        await dispatch(leaveBoardAsync(boardId!));
    }


    const handleSubmitCreateCustomCard = async (request: NewCardType) => {
        const model = {
            cardListId: cardLists!.find(cardList => cardList.title === todo_card_list)!.id,
            task: request.task,
            deadline: request.deadline,
            userId: request.isPublicCard === true ? null : userId!
        }
        const response = await dispatch(createCardAsync(model));

        if (createCardAsync.fulfilled.match(response)) {
            if (conn.current) {
                await conn.current.send("AddNewCard", {
                    boardId: boardId,
                    cardModel: {
                        cardListId: cardLists!.find(cardList => cardList.title === todo_card_list)!.id,
                        cardId: response.payload,
                        task: request.task,
                        deadline: request.deadline,
                        userId: request.isPublicCard === true ? null : userId!,
                        userAvatar: request.isPublicCard === true ? null : user?.avatarName,
                        userName: request.isPublicCard === true ? null : user?.email
                    }
                });
            }
            setOpenedCustomCardCreate(false);
        }
    }
    const handleSubmitGenerateCardsWithAI = async (request: GenerateCardsWithAIType) => {
        const model: ICreateCardWithAI = {
            boardId: boardId!,
            task: request.description
        }
        const response = await dispatch(generateCardsWithAIAsync(model));

        if (generateCardsWithAIAsync.fulfilled.match(response)) {
            console.log("User", user);
            console.log("response.payload", response.payload);
            if (conn.current) {
                await conn.current.send("AddNewCards", {
                    boardId: boardId,
                    cardModels: response.payload
                });
            }
            setOpenedAICardCreate(false);
        }
    }
    const handleSubmitAddMember = async (request: EmailValidationType) => {
        console.log("Hello");
        var response = await dispatch(addMemberToBoardAsync({
            boardId: boardId!,
            memberEmail: request.email
        }));

        if (addMemberToBoardAsync.fulfilled.match(response)) {
            /*dispatch(addInformation({
                message: `${response.payload} has been add to board.`,
                type: TypeOfInformation.Success
            }));*/
            if (conn.current) {
                conn.current.send("UserHasBeenAddToBoard", {
                    boardId: boardId!,
                    addedUserId: response.payload.userId,
                    addedUserEmail: response.payload.email,
                    addedUserAvatarName: response.payload.avatarName,
                    userEmailWhoAdd: user!.email
                })
            }
            setOpenedAddMember(false);

        }
        else {
            dispatch(addInformation({
                message: `${response.payload?.errors[0].code}`,
                type: TypeOfInformation.Error
            }));
        }
    }
    const handleRemoveMemberFromBoard = async (removedUserId: string, removedUserEmail: string) => {
        const response = await dispatch(removeMemberFromBoardAsync({
            boardId: boardId!,
            userId: removedUserId
        }));

        if (removeMemberFromBoardAsync.fulfilled.match(response)) {
            if (conn.current) {
                conn.current.send("UserHasBeenRemovedFromBoard", {
                    boardId: boardId,
                    removedUserId: removedUserId,
                    removedUserEmail: removedUserEmail,
                    userEmailWhoRemoved: user!.email,
                    cardsId: response.payload
                });
            }

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
        <GeneralMode isOpened={openedGeneralModal} selectedItem={null} onClose={() => {
            setOpenedGeneralModal(false);
        }}>
            <div className="board-members-items"
                ref={(ref) => {
                    boardMembersRef.current = ref;
                }}
                style={{
                    overflowY: boardMembersOverflowY
                }}
            >
                {membersOfBoard && membersOfBoard.map((element) => (
                    <div className="board-member-item" key={element.userId}>
                        <img src={`${baseUrl}/images/avatars/${element.avatarName}.png`} alt={`Avatar of ${element.email}`} />
                        <p>{element.email}</p>
                        {element.userId !== userId &&
                            <div className="buttons">
                                <button
                                    onClick={() => handleRemoveMemberFromBoard(element.userId, element.email)}
                                >Remove</button>
                            </div>}
                    </div>
                ))}
            </div>
        </GeneralMode>
        {information && (
            <InformationAlert message={information.message} type={information.type} />
        )}
        <div className="members-setting-container">
            {openedAddMember === true ?
                <form onSubmit={handleSubmit(handleSubmitAddMember)}>
                    <input {...register("email")} type="text" placeholder="example@mail.com" style={{ borderColor: errors.email ? "red" : "#6822ca" }} />
                    <button type="submit">
                        <p>Add</p>
                    </button>
                    <button
                        onClick={() => {
                            setOpenedAddMember(false);
                            setValue("email", "");
                        }}
                    >
                        <p>Cancel</p>
                    </button>
                </form>
                :
                <button
                    onMouseEnter={() => {
                        setButtonHovered("add_member")
                    }}
                    onMouseLeave={() => {
                        setButtonHovered(null);
                    }}
                    onClick={() => setOpenedAddMember(true)}
                >
                    <img src={buttonHovered === "add_member" ? add_member_white_icon : add_member_purple_icon} alt="Add Member" />
                    <p>Add members</p>
                </button>}
            <button
                onMouseEnter={() => {
                    setButtonHovered("members")
                }}
                onMouseLeave={() => {
                    setButtonHovered(null);
                }}
                onClick={() => setOpenedGeneralModal(true)}
            >
                <img src={buttonHovered === "members" ? members_white_icon : members_purple_icon} alt="Members" />
                <p>Members</p>
            </button>
            <button
                onMouseEnter={() => {
                    setButtonHovered("leave")
                }}
                onMouseLeave={() => {
                    setButtonHovered(null);
                }}
                onClick={leaveBoard}
            >
                <img src={buttonHovered === "leave" ? leave_board_white_icon : leave_board_purple_icon} alt="Leave board" />
                <p>Leave</p>
            </button>
        </div>
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
                        {element.title === todo_card_list && createNewCards()}
                    </div>

                </div>
            ))}
        </div>
    </div >
}