import { TypedUseSelectorHook } from "react-redux";
<<<<<<< HEAD
import type { RootState } from "../redux/store.ts";
=======
import type { RootState } from "./store.ts";
>>>>>>> d03efc386315301a4c81be8b9cc25da9c7260788
export declare const useRootState: import("react-redux").UseSelector<{
    authenticate: import("../interfaces/authenticateInterfaces.ts").IAuthenticateInitialState;
    general: import("../interfaces/generalInterface.ts").IGeneralInitialState;
    board: import("../interfaces/boardInterface.ts").IBoardInitialState;
    table: import("../interfaces/tableInterface.ts").ITableInitialState;
    card: {};
    gemini: {};
    feedback: import("../interfaces/feedbackInterface.ts").IFeedbackInitialState;
}>;
export declare const useAppDispatch: import("react-redux").UseDispatch<import("redux-thunk").ThunkDispatch<{
    authenticate: import("../interfaces/authenticateInterfaces.ts").IAuthenticateInitialState;
    general: import("../interfaces/generalInterface.ts").IGeneralInitialState;
    board: import("../interfaces/boardInterface.ts").IBoardInitialState;
    table: import("../interfaces/tableInterface.ts").ITableInitialState;
    card: {};
    gemini: {};
    feedback: import("../interfaces/feedbackInterface.ts").IFeedbackInitialState;
}, undefined, import("redux").UnknownAction> & import("redux").Dispatch<import("redux").UnknownAction>>;
export declare const useAppSelector: TypedUseSelectorHook<RootState>;
