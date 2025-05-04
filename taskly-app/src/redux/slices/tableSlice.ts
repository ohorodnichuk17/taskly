import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import {
    ITable,
    ITableEdit,
    ITableInitialState,
    ITableItem, ITableItemEdit,
    IUserListForTable
} from "../../interfaces/tableInterface.ts";
import {
    addUserToTable,
    createTable, createTableItem,
    deleteTable, deleteTableItem,
    editTable, editTableItem, getAllMembersInTable,
    getTableById,
    getTableItems,
    getTablesByUser, markTableItemAsCompleted, removeUserFromTable
} from "../actions/tablesAction.ts";

const initialState: ITableInitialState = {
    listOfTables: null,
    tableItems: null,
    membersList: [],
    createTableError: null,
    createTableItemError: null,
    deleteTableError: null,
    deleteTableItemError: null,
    editTableError: null,
    editTableItemError: null,
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
            .addCase(getTableItems.fulfilled, (state, action: PayloadAction<{ $values: ITableItem[] }>) => {
                state.tableItems = action.payload?.$values ?? [];
            })
            .addCase(getTableItems.rejected, (state) => {
                state.tableItems = null;
            })
            .addCase(getTablesByUser.rejected, (state) => {
                state.listOfTables = null;
            })
            .addCase(getTableById.fulfilled, (state, action: PayloadAction<ITable>) => {
                if (state.listOfTables) {
                    const tableIndex = state.listOfTables.findIndex((table) => table.id === action.payload.id);
                    if (tableIndex !== undefined && tableIndex !== -1) {
                        state.listOfTables[tableIndex] = action.payload;
                    }
                }
            })
            .addCase(getTableById.rejected, (state) => {
                state.listOfTables = null;
            })
            .addCase(createTable.fulfilled, (state, action: PayloadAction<ITable>) => {
                state.listOfTables = state.listOfTables ? [...state.listOfTables, action.payload] : [action.payload];
            })
            .addCase(createTable.rejected, (state, action) => {
                state.createTableError = action.payload;
            })
            .addCase(createTableItem.fulfilled, (state, action: PayloadAction<ITableItem>) => {
                if (state.tableItems) {
                    state.tableItems = [...state.tableItems, action.payload];
                } else {
                    state.tableItems = [action.payload];
                }
            })
            .addCase(createTableItem.rejected, (state, action) => {
                state.createTableItemError = action.payload;
            })
            .addCase(deleteTable.fulfilled, (state, action: PayloadAction<boolean, string, { arg: string; requestId: string; requestStatus: "fulfilled"; }, never>) => {
                const tableIdToDelete = action.meta.arg;
                if (state.listOfTables && tableIdToDelete) {
                    state.listOfTables = state.listOfTables.filter((table) => table.id !== tableIdToDelete);
                }
            })
            .addCase(deleteTable.rejected, (state, action) => {
                state.deleteTableError = action.payload;
            })
            .addCase(deleteTableItem.fulfilled, (state, action: PayloadAction<boolean, string, { arg: string; requestId: string; requestStatus: "fulfilled"; }, never>) => {
                const itemIdToDelete = action.meta.arg;
                if (state.tableItems && itemIdToDelete) {
                    state.tableItems = state.tableItems.filter((item) => item.id !== itemIdToDelete);
                }
            })
            .addCase(deleteTableItem.rejected, (state, action) => {
                state.deleteTableItemError = action.payload;
            })
            .addCase(markTableItemAsCompleted.fulfilled, (state, action: PayloadAction<boolean, string, { arg: { tableItemId: string, isCompleted: boolean }; requestId: string; requestStatus: "fulfilled"; }, never>) => {
                const { tableItemId, isCompleted } = action.meta.arg;
                if (state.tableItems) {
                    const item = state.tableItems.find((item) => item.id === tableItemId);
                    if (item) {
                        item.isCompleted = isCompleted;
                        item.status = isCompleted ? "Done" : item.status;
                    }
                }
            })
            .addCase(markTableItemAsCompleted.rejected, (state, action) => {
                state.deleteTableItemError = action.payload;
            })
            .addCase(editTableItem.fulfilled, (state, action: PayloadAction<ITableItemEdit>) => {
                if (state.tableItems) {
                    const updatedItemIndex = state.tableItems.findIndex(
                        (item) => item.id === action.payload.id
                    );
                    if (updatedItemIndex !== -1) {
                        state.tableItems[updatedItemIndex] = {
                            ...state.tableItems[updatedItemIndex],
                            ...action.payload,
                        };
                    }
                }
            })
            .addCase(editTableItem.rejected, (state, action) => {
                state.editTableItemError = action.payload;
            })
            .addCase(addUserToTable.fulfilled, () => {

            })
            .addCase(addUserToTable.rejected, () => {

            })
            .addCase(removeUserFromTable.fulfilled, () => {

            })
            .addCase(removeUserFromTable.rejected, () => {

            })
            .addCase(getAllMembersInTable.fulfilled, (state, action: PayloadAction<IUserListForTable[]>) => {
                state.membersList = action.payload;
            })
            .addCase(getAllMembersInTable.rejected, (state) => {
                state.membersList = null;
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