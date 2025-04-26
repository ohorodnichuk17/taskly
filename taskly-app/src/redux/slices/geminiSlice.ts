import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { ICreatedCard } from "../../interfaces/cardsInterface";
import { generateCardsWithAIAsync } from "../actions/geminiActions";

const geminiSlice = createSlice({
    name: "geminiSlice",
    initialState: {},
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(generateCardsWithAIAsync.fulfilled, (state, action: PayloadAction<ICreatedCard[]>) => {

            })
            .addCase(generateCardsWithAIAsync.rejected, (state) => {

            })
    }
});

export const geminiReducer = geminiSlice.reducer;