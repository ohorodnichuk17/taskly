import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { authenticateReducer } from "./slices/authenticateSlice.ts";
import { generalReducer } from './slices/generalSlice.ts';
import { boardReducer } from './slices/boardSlice.ts';
import { tableReducer } from "./slices/tableSlice.ts";
import { cardReducer } from './slices/cardSlice.ts';
import { geminiReducer } from './slices/geminiSlice.ts';
import { feedbackReducer } from "./slices/feedbackSlice.ts";
import { achievementsReducer } from './slices/achievementsSlice.ts';

const reducers = combineReducers({
    authenticate: authenticateReducer,
    general: generalReducer,
    board: boardReducer,
    table: tableReducer,
    card: cardReducer,
    gemini: geminiReducer,
    feedback: feedbackReducer,
    achievements: achievementsReducer
});

export const store = configureStore({
    reducer: reducers
});

/*const authenticateConfig = {
    key: "authenticate", // Під яким ключем буде зберігатися стан редюсера
    storage: storage, // localStorage, щоб був sessionStorage, трема - storageSession
}

const reducers = combineReducers({
    authenticate: persistReducer(authenticateConfig, authenticateReducer),
    general: generalReducer
})
export const store = configureStore({
    reducer: reducers,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware({
            serializableCheck: {
                ignoredActions: [
                    'persist/PERSIST',
                    'persist/REHYDRATE',
                    'persist/FLUSH',
                    'persist/PAUSE',
                    'persist/PURGE',
                    'persist/REGISTER',
                ]
            }
        })
})

export const persistor = persistStore(store);
purgeStoredState(authenticateConfig);*/

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch; 