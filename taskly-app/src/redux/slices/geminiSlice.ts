import { createSlice } from "@reduxjs/toolkit";
import { generateCardsWithAIAsync } from "../actions/geminiActions";

const geminiSlice = createSlice({
    name: "geminiSlice",
    initialState: {},
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(generateCardsWithAIAsync.fulfilled, () => {

            })
            .addCase(generateCardsWithAIAsync.rejected, () => {

            })
    }
});

export const geminiReducer = geminiSlice.reducer;