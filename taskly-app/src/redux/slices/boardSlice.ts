import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IBoardInitialState, ICard, ICardListItem, IUsersBoard } from "../../interfaces/boardInterface";
import { getBoardsByUserAsync, getCardsListsByBoardIdAsync } from "../actions/boardsAction";
import { IValidationErrors } from "../../interfaces/generalInterface";

const findAndRemoveItemFromArray = (condition: (c: any) => boolean, array: any[]) => {
    let item = array.find(condition);
    if (item) {
        let index = array.indexOf(item);
        if (index !== -1) {
            return [...array.slice(0, index), ...array.slice(index + 1)];
        }
    }
    return array;
}

const addItemToArrayFromAnotherArray = (item: any, array: any[]) => {
    console.log("addItemToArrayFromAnotherArray");
    return [...array, item];
}

const initialState: IBoardInitialState = {
    listOfBoards: null,
    cardList: null
}

const boardSlice = createSlice({
    name: "boardSlice",
    initialState: initialState,
    reducers: {
    },
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