import { IAchievement } from "../../interfaces/achievementsInterface";
import { IValidationErrors } from "../../interfaces/generalInterface";
export declare const getAllAchievementsAsync: import("@reduxjs/toolkit").AsyncThunk<IAchievement[], void, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
