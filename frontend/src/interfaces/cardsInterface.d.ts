export interface ICreateCard {
    cardListId: string;
    task: string;
    deadline: Date;
    userId: string | null;
}
export interface ICreatedCard {
    cardId: string;
    cardListId: string;
    task: string;
    deadline: Date;
    userId: string | null;
    userAvatar: string | null;
    userName: string | null;
}
export interface ICreateCardWithAI {
    boardId: string;
    task: string;
}
