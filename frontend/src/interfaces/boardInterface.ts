
export interface IBoardInitialState {
    listOfBoards: IUsersBoard[] | null,
    cardList: ICardListItem[] | null,
    cardsOfLeavedUser: string[] | null,
    cardsOfRemovedUser: string[] | null,
    membersOfBoard: IMemberOfBoard[] | null,
    templatesOfBoard: ITemplateOfBoard[] | null
}

export interface IUsersBoard {
    id: string,
    name: string,
    countOfMembers: number,
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

export interface IAddMemberRequest {
    boardId: string,
    memberEmail: string
}

export interface IMemberOfBoard {
    userId: string,
    email: string,
    avatarName: string
}

export interface IRemoveMemberFromBoard {
    boardId: string,
    userId: string
}

export interface ITemplateOfBoard {
    id: string,
    name: string,
    imagePath: string
}

export interface ICreateBoard {
    userId: string,
    name: string,
    tag: string | null,
    isTeamBoard: boolean,
    boadrTemplateId: string
}