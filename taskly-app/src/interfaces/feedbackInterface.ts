export interface IFeedbackInitialState {
    listOfFeedbacks: IFeedback[] | null,
}

export interface IFeedbackResponse {
    id: string;
    userId: string;
    rating: number;
    review: string;
    createdAt: Date;
}

export interface IFeedback {
    id: string;
    userId: string;
    review: string;
    rating: number;  // Rating given by the user (e.g., 1 to 5 stars)
    createdAt: Date;
}