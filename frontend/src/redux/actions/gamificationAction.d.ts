import { IBadgeResponse, IChallengeResponse, ICreateChallengeRequest, ICreateChallengeResponse, IUserBadgeResponse } from "../../interfaces/gamificationInterface.ts";
import { IValidationErrors } from "../../interfaces/generalInterface.ts";
export declare const createChallengeAsync: import("@reduxjs/toolkit").AsyncThunk<ICreateChallengeResponse, ICreateChallengeRequest, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const deleteChallengeAsync: import("@reduxjs/toolkit").AsyncThunk<boolean, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getAllChallengesAsync: import("@reduxjs/toolkit").AsyncThunk<IChallengeResponse[], void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getActiveChallengesAsync: import("@reduxjs/toolkit").AsyncThunk<IChallengeResponse[], void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getAvailableChallengesAsync: import("@reduxjs/toolkit").AsyncThunk<IChallengeResponse[], void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getChallengeByIdAsync: import("@reduxjs/toolkit").AsyncThunk<IChallengeResponse, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const bookChallengeAsCompletedAsync: import("@reduxjs/toolkit").AsyncThunk<boolean, {
    challengeId: string;
    userId: string;
}, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const markChallengeAsCompletedAsync: import("@reduxjs/toolkit").AsyncThunk<boolean, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getAllBadgesAsync: import("@reduxjs/toolkit").AsyncThunk<IBadgeResponse[], void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getBadgeByIdAsync: import("@reduxjs/toolkit").AsyncThunk<IBadgeResponse, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getUserLevelAsync: import("@reduxjs/toolkit").AsyncThunk<number, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getAllBadgesByUserIdAsync: import("@reduxjs/toolkit").AsyncThunk<IUserBadgeResponse[], string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getUserBadgeByUserIdAndBadgeIdAsync: import("@reduxjs/toolkit").AsyncThunk<IUserBadgeResponse, {
    userId: string;
    badgeId: string;
}, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
