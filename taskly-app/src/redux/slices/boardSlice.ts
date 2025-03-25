import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IBoardInitialState, ICardListItem, IUsersBoard } from "../../interfaces/boardInterface";
import { getBoardsByUserAsync, getCardsListsByBoardIdAsync } from "../actions/boardsAction";
import { IValidationErrors } from "../../interfaces/generalInterface";

const initialState: IBoardInitialState = {
    listOfBoards: null,
    cardList: null
}

const boardSlice = createSlice({
    name: "boardSlice",
    initialState: initialState,
    reducers: {},
    extraReducers(builder) {
        builder
            .addCase(getBoardsByUserAsync.fulfilled, (state, action: PayloadAction<IUsersBoard[]>) => {
                state.listOfBoards = action.payload;
            })
            .addCase(getBoardsByUserAsync.rejected, (state) => {

            })
            .addCase(getCardsListsByBoardIdAsync.fulfilled, (state, action: PayloadAction<ICardListItem[]>) => {
                state.cardList = action.payload;
            })
            .addCase(getCardsListsByBoardIdAsync.rejected, (state) => {

            })

    },
});

export const boardReducer = boardSlice.reducer; 