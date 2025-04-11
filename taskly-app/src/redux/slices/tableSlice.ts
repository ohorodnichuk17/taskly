import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import {ITable, ITableInitialState, ITableItem} from "../../interfaces/tableInterface.ts";
import {getTableItems, getTablesByUser} from "../actions/tablesAction.ts";

const initialState: ITableInitialState = {
    listOfTables: null,
    tableItems: null
}

const tableSlice = createSlice({
    name: "tableSlice",
    initialState: initialState,
    reducers: {
    },
    extraReducers(builder) {
        builder
            .addCase(getTablesByUser.fulfilled, (state, action: PayloadAction<ITable[]>) => {
                state.listOfTables = action.payload;
            })
            .addCase(getTableItems.fulfilled, (state, action: PayloadAction<ITableItem[]>) => {
                state.tableItems = action.payload;
            })
    },
});

export const tableReducer = tableSlice.reducer;