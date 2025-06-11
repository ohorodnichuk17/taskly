export declare const useRootState: import("react-redux").UseSelector<{
    authenticate: import("../interfaces/authenticateInterfaces.ts").IAuthenticateInitialState;
    general: import("../interfaces/generalInterface.ts").IGeneralInitialState;
    board: import("../interfaces/boardInterface.ts").IBoardInitialState;
    table: import("../interfaces/tableInterface.ts").ITableInitialState;
    card: {};
    gemini: {};
    feedback: import("../interfaces/feedbackInterface.ts").IFeedbackInitialState;
    achievements: import("../interfaces/achievementsInterface.ts").IAchievementsInitialState;
    gamification: import("../interfaces/gamificationInterface.ts").IGamificationInitialState;
    cardComments: import("../interfaces/cardCommentsInterface.ts").ICardCommentsInitialState;
}>;
export declare const useAppDispatch: import("react-redux").UseDispatch<import("redux-thunk").ThunkDispatch<{
    authenticate: import("../interfaces/authenticateInterfaces.ts").IAuthenticateInitialState;
    general: import("../interfaces/generalInterface.ts").IGeneralInitialState;
    board: import("../interfaces/boardInterface.ts").IBoardInitialState;
    table: import("../interfaces/tableInterface.ts").ITableInitialState;
    card: {};
    gemini: {};
    feedback: import("../interfaces/feedbackInterface.ts").IFeedbackInitialState;
    achievements: import("../interfaces/achievementsInterface.ts").IAchievementsInitialState;
    gamification: import("../interfaces/gamificationInterface.ts").IGamificationInitialState;
    cardComments: import("../interfaces/cardCommentsInterface.ts").ICardCommentsInitialState;
}, undefined, import("redux").UnknownAction> & import("redux").Dispatch<import("redux").UnknownAction>>;
