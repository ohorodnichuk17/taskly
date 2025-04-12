import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import {ITable, ITableCreate, ITableInitialState, ITableItem} from "../../interfaces/tableInterface.ts";
import {createTable, deleteTable, getTableItems, getTablesByUser} from "../actions/tablesAction.ts";

const initialState: ITableInitialState = {
    listOfTables: null,
    tableItems: null,
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
            .addCase(getTableItems.rejected, (state) => {
                state.tableItems = null;
            })
            .addCase(getTablesByUser.rejected, (state) => {
                state.listOfTables = null;
            })
            .addCase(createTable.fulfilled, (state, action: PayloadAction<ITableCreate>) => {
                state.listOfTables = state.listOfTables ? [...state.listOfTables, action.payload] : [action.payload];
            })
            .addCase(createTable.rejected, (state, action) => {
                state.createTableError = action.payload;
            })
            .addCase(deleteTable.fulfilled, (state, action: PayloadAction<string>) => {
                if (state.listOfTables) {
                    state.listOfTables = state.listOfTables.filter((table) => table.id !== action.payload);
           }})
            .addCase(deleteTable.rejected, (state, action) => {
                state.deleteTableError = action.payload;
            });
    },
});

export const tableReducer = tableSlice.reducer;