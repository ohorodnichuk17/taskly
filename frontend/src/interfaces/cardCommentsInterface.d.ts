export interface ICardCommentsInitialState {
    cardComments: ICardComment[] | null;
}
export interface ICardComment {
    id: string;
    text: string;
    userName: string;
    userAvatar: string;
    userId: string;
    createdAt: Date;
}
