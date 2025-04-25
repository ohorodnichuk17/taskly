import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { createCardAsync } from "../actions/cardsActions";

const cardSlice = createSlice({
    name: "cardSlice",
    initialState: {},
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(createCardAsync.fulfilled, (state, action: PayloadAction<string>) => {

            })
            .addCase(createCardAsync.rejected, (state) => {

            })
    }
});

export const cardReducer = cardSlice.reducer;