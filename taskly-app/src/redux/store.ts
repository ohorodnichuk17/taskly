import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { authenticateReducer } from "../redux/slices/authenticateSlice.ts";
import { generalReducer } from './slices/generalSlice.ts';
import { boardReducer } from './slices/boardSlice.ts';


const reducers = combineReducers({
    authenticate: authenticateReducer,
    general: generalReducer,
    board: boardReducer
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