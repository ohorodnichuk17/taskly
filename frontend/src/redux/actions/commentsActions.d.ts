import { ICardComment } from "../../interfaces/cardCommentsInterface";
import { IValidationErrors } from "../../interfaces/generalInterface";
export declare const getCommentsByCardIdAsync: import("@reduxjs/toolkit").AsyncThunk<ICardComment[], string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
