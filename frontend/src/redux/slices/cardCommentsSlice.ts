import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { ICardComment, ICardCommentsInitialState } from "../../interfaces/cardCommentsInterface";
import { getCommentsByCardIdAsync } from "../actions/commentsActions";

const initialState: ICardCommentsInitialState = {
    cardComments: null
};

const cardCommentsSlice = createSlice({
    name: "commentsSlice",
    initialState: initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(getCommentsByCardIdAsync.fulfilled, (state, action: PayloadAction<ICardComment[]>) => {
            state.cardComments = action.payload;
        })
    }
});

export const cardCommentsReducer = cardCommentsSlice.reducer;