import { configureStore } from '@reduxjs/toolkit';
import { authenticateReducer } from "../redux/slices/authenticateSlice.ts";

export const store = configureStore({
    reducer: {
        authenticateReducer
    }
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch; 