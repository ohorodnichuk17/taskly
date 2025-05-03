import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IBoardInitialState, ICardListItem, IMemberOfBoard, ITemplateOfBoard, IUsersBoard } from "../../interfaces/boardInterface";
import { getBoardsByUserAsync, getCardsListsByBoardIdAsync, getMembersOfBoardAsync, getTemplatesOfBoardAsync, leaveBoardAsync, removeMemberFromBoardAsync } from "../actions/boardsAction";


const initialState: IBoardInitialState = {
    listOfBoards: null,
    cardList: null,
    cardsOfLeavedUser: null,
    cardsOfRemovedUser: null,
    membersOfBoard: null,
    templatesOfBoard: null
}

const boardSlice = createSlice({
    name: "boardSlice",
    initialState: initialState,
    reducers: {
        removeCardsOfLeavedUser: (state) => {
            state.cardsOfLeavedUser = null;
        }
    },
    extraReducers(builder) {
        builder
            .addCase(getBoardsByUserAsync.fulfilled, (state, action: PayloadAction<IUsersBoard[]>) => {
                state.listOfBoards = action.payload;
            })
            .addCase(getCardsListsByBoardIdAsync.fulfilled, (state, action: PayloadAction<ICardListItem[]>) => {
                state.cardList = action.payload;
            })
            .addCase(leaveBoardAsync.fulfilled, (state, action: PayloadAction<string[]>) => {
                state.cardsOfLeavedUser = action.payload;
            })
            .addCase(getMembersOfBoardAsync.fulfilled, (state, action: PayloadAction<IMemberOfBoard[]>) => {
                state.membersOfBoard = action.payload;
            })
            .addCase(removeMemberFromBoardAsync.fulfilled, (state, action: PayloadAction<string[]>) => {
                state.cardsOfRemovedUser = action.payload;
            })
            .addCase(getTemplatesOfBoardAsync.fulfilled, (state, action: PayloadAction<ITemplateOfBoard[]>) => {
                state.templatesOfBoard = action.payload;
            })



    },
});

export const boardReducer = boardSlice.reducer;
export const { removeCardsOfLeavedUser } = boardSlice.actions;