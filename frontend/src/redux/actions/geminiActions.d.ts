import { ICreateCardWithAI, ICreatedCard } from "../../interfaces/cardsInterface";
import { IValidationErrors } from "../../interfaces/generalInterface";
export declare const generateCardsWithAIAsync: import("@reduxjs/toolkit").AsyncThunk<ICreatedCard[], ICreateCardWithAI, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
