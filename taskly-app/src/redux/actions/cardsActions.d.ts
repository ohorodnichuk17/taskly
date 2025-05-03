import { IValidationErrors } from "../../interfaces/generalInterface";
import { ICreateCard } from "../../interfaces/cardsInterface";
export declare const createCardAsync: import("@reduxjs/toolkit").AsyncThunk<string, ICreateCard, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
