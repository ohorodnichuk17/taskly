export interface IFeedbackInitialState {
    listOfFeedbacks: IFeedback[] | null,
    createFeedbackError: string | null,
    deleteFeedbackError: string | null,
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
    user?: IFeedbackUser
}

export interface ICreateFeedback {
    userId: string;
    review: string;
    rating: number;
    createdAt: Date;
    user?: IFeedbackUser
}

export interface IFeedbackUser {
    userName?: string;
    email?: string;
    avatarId: string;
}