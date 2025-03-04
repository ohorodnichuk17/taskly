import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IBoardInitialState, IUsersBoard } from "../../interfaces/boardInterface";
import { getBoardsByUserAsync } from "../actions/boardsAction";

const initialState: IBoardInitialState = {
    listOfBoards: null
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
    },
});

export const boardReducer = boardSlice.reducer; 