export interface IFeedbackInitialState {
    listOfFeedbacks: IFeedback[] | null;
}
export interface IFeedbackResponse {
    id: string;
    userId: string;
    rating: number;
    review: string;
    createdAt: Date;
    user: IFeedbackUser;
}
export interface IFeedback {
    id: string;
    userId: string;
    review: string;
    rating: number;
    createdAt: Date;
<<<<<<< HEAD
=======
    user?: IFeedbackUser;
}
export interface ICreateFeedback {
    userId: string;
    review: string;
    rating: number;
    createdAt: Date;
    user?: IFeedbackUser;
>>>>>>> d03efc386315301a4c81be8b9cc25da9c7260788
}
export interface IFeedbackUser {
    userName?: string;
    email?: string;
    avatarId: string;
}
