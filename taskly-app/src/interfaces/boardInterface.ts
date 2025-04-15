
export interface IBoardInitialState {
    listOfBoards: IUsersBoard[] | null,
    cardList: ICardListItem[] | null
}

export interface IUsersBoard {
    id: string,
    name: string,
    countOfMemebers: number,
    boardTemplateName: string,
    boardTemplateColor: string
}

export interface IComment {
    id: string,
    text: string | null,
    createdAt: Date
}
export interface ICard {
    id: string,
    title: string | null,
    description: string,
    attachmentUrl: string | null,
    userId: string | null,
    isCompleated: boolean,
    userAvatar: string | null,
    userName: string | null,
    status: string,
    startTime: Date,
    endTime: Date,
    comments: IComment[] | null
}

export interface ICardListItem {
    id: string,
    title: string,
    cards: ICard[] | null
    boardId: string
}