import { IValidationErrors } from '../../interfaces/generalInterface';
import { IAvatar, IChangePasswordRequest, ICheckHasUserSentRequestToChangePassword, IEditAvatar, ILoginRequest, IRegisterRequest, ISetUserNameForSolanaUser, ISolanaUserProfile, IUserProfile, IVerificateEmailRequest } from '../../interfaces/authenticateInterfaces';
export declare const sendVerificationCodeAsync: import("@reduxjs/toolkit").AsyncThunk<string, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const verificateEmailAsync: import("@reduxjs/toolkit").AsyncThunk<string, IVerificateEmailRequest, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getAllAvatarsAsync: import("@reduxjs/toolkit").AsyncThunk<IAvatar[], void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const registerAsync: import("@reduxjs/toolkit").AsyncThunk<void, IRegisterRequest, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const loginAsync: import("@reduxjs/toolkit").AsyncThunk<IUserProfile, ILoginRequest, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const checkTokenAsync: import("@reduxjs/toolkit").AsyncThunk<IUserProfile, void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const sendRequestToChangePasswordAsync: import("@reduxjs/toolkit").AsyncThunk<string, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const checkHasUserSentRequestToChangePasswordAsync: import("@reduxjs/toolkit").AsyncThunk<string | null, ICheckHasUserSentRequestToChangePassword, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const editAvatarAsync: import("@reduxjs/toolkit").AsyncThunk<{
    avatarId: string;
}, IEditAvatar, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const changePasswordAsync: import("@reduxjs/toolkit").AsyncThunk<string, IChangePasswordRequest, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const logoutAsync: import("@reduxjs/toolkit").AsyncThunk<void, void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const solanaLogoutAsync: import("@reduxjs/toolkit").AsyncThunk<void, void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const solanaWalletAuthAsync: import("@reduxjs/toolkit").AsyncThunk<ISolanaUserProfile, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const setUserNameForSolanaUserAsync: import("@reduxjs/toolkit").AsyncThunk<string, ISetUserNameForSolanaUser, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const checkSolanaTokenAsync: import("@reduxjs/toolkit").AsyncThunk<ISolanaUserProfile, void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
