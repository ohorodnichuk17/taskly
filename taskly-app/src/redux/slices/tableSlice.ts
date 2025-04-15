import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import {ITable, ITableCreate, ITableEdit, ITableInitialState, ITableItem} from "../../interfaces/tableInterface.ts";
import {
    addTableItem,
    createTable,
    deleteTable,
    editTable,
    getTableById,
    getTableItems,
    getTablesByUser
} from "../actions/tablesAction.ts";

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
                state.tableItems = action.payload?.$values ?? [];
            })
            .addCase(getTableItems.rejected, (state) => {
                state.tableItems = null;
            })
            .addCase(getTablesByUser.rejected, (state) => {
                state.listOfTables = null;
            })
            .addCase(getTableById.fulfilled, (state, action: PayloadAction<ITable>) => {
                const tableIndex = state.listOfTables?.findIndex((table) => table.id === action.payload.id);
                if (tableIndex !== undefined && tableIndex !== -1) {
                    state.listOfTables[tableIndex] = action.payload;
                }
            })
            .addCase(getTableById.rejected, (state) => {
                state.listOfTables = null;
            })
            .addCase(addTableItem.fulfilled, (state, action: PayloadAction<ITableItem>) => {
                if (state.tableItems) {
                    state.tableItems = [...state.tableItems, action.payload];
                } else {
                    state.tableItems = [action.payload];
                }
            })
            .addCase(addTableItem.rejected, (state) => {
                state.tableItems = null;
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
            })
            .addCase(editTable.fulfilled, (state, action: PayloadAction<ITableEdit>) => {
                if (state.listOfTables) {
                    const updatedTableIndex = state.listOfTables.findIndex(
                        (table) => table.id === action.payload.id
                    );
                    if (updatedTableIndex !== -1) {
                        state.listOfTables[updatedTableIndex] = {
                            ...state.listOfTables[updatedTableIndex],
                            ...action.payload,
                        };
                    }
                }
            })
            .addCase(editTable.rejected, (state, action) => {
                state.editTableError = action.payload;
            });
    },
});

export const tableReducer = tableSlice.reducer;