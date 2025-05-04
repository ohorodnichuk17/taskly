<<<<<<< HEAD
import { IFeedback, IFeedbackResponse } from "../../interfaces/feedbackInterface.ts";
=======
import { ICreateFeedback, IFeedback, IFeedbackResponse } from "../../interfaces/feedbackInterface.ts";
>>>>>>> d03efc386315301a4c81be8b9cc25da9c7260788
export declare const getAllFeedbacksAsync: import("@reduxjs/toolkit").AsyncThunk<IFeedbackResponse[], void, {
    rejectValue: string;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getFeedbackByIdAsync: import("@reduxjs/toolkit").AsyncThunk<IFeedbackResponse, string, {
    rejectValue: string;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
<<<<<<< HEAD
export declare const createFeedbackAsync: import("@reduxjs/toolkit").AsyncThunk<IFeedback, IFeedback, {
    rejectValue: string;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
=======
export declare const createFeedbackAsync: import("@reduxjs/toolkit").AsyncThunk<IFeedback, ICreateFeedback, {
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction>;
    extra?: unknown;
    rejectValue?: unknown;
>>>>>>> d03efc386315301a4c81be8b9cc25da9c7260788
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const deleteFeedbackAsync: import("@reduxjs/toolkit").AsyncThunk<boolean, string, {
    rejectValue: string;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
