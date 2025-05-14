export declare const store: import("@reduxjs/toolkit").EnhancedStore<{
    authenticate: import("../interfaces/authenticateInterfaces.ts").IAuthenticateInitialState;
    general: import("../interfaces/generalInterface.ts").IGeneralInitialState;
    board: import("../interfaces/boardInterface.ts").IBoardInitialState;
    table: import("../interfaces/tableInterface.ts").ITableInitialState;
    card: {};
    gemini: {};
    feedback: import("../interfaces/feedbackInterface.ts").IFeedbackInitialState;
    achievements: import("../interfaces/achievementsInterface.ts").IAchievementsInitialState;
    gamification: import("../interfaces/gamificationInterface.ts").IGamificationInitialState;
}, import("redux").UnknownAction, import("@reduxjs/toolkit").Tuple<[import("redux").StoreEnhancer<{
    dispatch: import("redux-thunk").ThunkDispatch<{
        authenticate: import("../interfaces/authenticateInterfaces.ts").IAuthenticateInitialState;
        general: import("../interfaces/generalInterface.ts").IGeneralInitialState;
        board: import("../interfaces/boardInterface.ts").IBoardInitialState;
        table: import("../interfaces/tableInterface.ts").ITableInitialState;
        card: {};
        gemini: {};
        feedback: import("../interfaces/feedbackInterface.ts").IFeedbackInitialState;
        achievements: import("../interfaces/achievementsInterface.ts").IAchievementsInitialState;
        gamification: import("../interfaces/gamificationInterface.ts").IGamificationInitialState;
    }, undefined, import("redux").UnknownAction>;
}>, import("redux").StoreEnhancer]>>;
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
