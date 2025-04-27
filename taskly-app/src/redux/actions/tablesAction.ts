import {createAsyncThunk} from "@reduxjs/toolkit";
import {
    IUserToTable,
    ITable,
    ITableCreate,
    ITableEdit,
    ITableItem,
    ITableItemCreate,
    ITableItemEdit, IUserListForTable
} from "../../interfaces/tableInterface.ts";
import {IValidationErrors} from "../../interfaces/generalInterface.ts";
import {AxiosError} from "axios";
import {api} from "../../axios/api.ts";

export const getTablesByUser = createAsyncThunk<
    ITable[],
    string,
    {rejectValue: IValidationErrors }> (
    "table/get-tables-by-user-id",
    async (userId, {rejectWithValue}) => {
        try {
            var response = await api.get(`api/table/get-tables-by-user-id/?userId=${userId}`,
                {withCredentials: true}
            );
            return response.data.$values;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if(!error.response)
                throw err;
            return rejectWithValue(error.response.data);
        }
    }
)

export const getTableById = createAsyncThunk<
    ITable,
    string,
    {rejectValue: IValidationErrors}> (
    "table/get-table-by-id",
    async (tableId, {rejectWithValue}) => {
        try {
            var response = await api.get(`api/table/get-table-by-id/${tableId}`,
                {withCredentials: true}
            );
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;
            return rejectWithValue(error.response.data);
        }
    }
)

export const getTableItems = createAsyncThunk<
    ITableItem[],
    string,
    { rejectValue: IValidationErrors }
>(
    "table/get-all-table-items",
    async (tableId, { rejectWithValue }) => {
        try {
            const response = await api.get("api/table/get-all-table-items", {
                params: { toDoTableId: tableId },
                withCredentials: true,
            });
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
);

export const createTable = createAsyncThunk<
    ITableCreate,
    { name: string, userId: string },
    { rejectValue: IValidationErrors }
>(
    "table/create-table",
    async ({ name, userId }, { rejectWithValue }) => {
        try {
            console.log("Creating table with:", { name, userId });

            const response = await api.post("api/table/create-table",
                { name, userId },
                { withCredentials: true }
            );
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
)

export const deleteTable = createAsyncThunk<
    boolean,
    string,
    { rejectValue: IValidationErrors }
> (
    "table/delete-table",
    async (tableId, { rejectWithValue }) => {
        try {
            const response = await api.delete(`api/table/delete-table`, {
                params: { tableId },
                withCredentials: true
            });
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
)

export const deleteTableItem = createAsyncThunk<
    boolean,
    string,
    { rejectValue: IValidationErrors }
> (
    "table/delete-table-item",
    async (tableItemId, { rejectWithValue }) => {
        try {
            const response = await api.delete(`api/table/delete-table-item`, {
                params: {tableItemId},
                withCredentials: true
            });
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
)

export const editTable = createAsyncThunk<
    ITableEdit,
    { tableId: string; tableName: string },
    { rejectValue: IValidationErrors }
>(
    "table/edit-table",
    async ({ tableId, tableName }, { rejectWithValue }) => {
        try {
            const response = await api.put(
                "api/table/edit-table",
                { tableId, tableName },
                { withCredentials: true }
            );
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
);

export const markTableItemAsCompleted = createAsyncThunk<
    boolean,
    { tableItemId: string, isCompleted: boolean },
    { rejectValue: IValidationErrors }
>(
    "table/mark-as-completed",
    async ({ tableItemId, isCompleted }, { rejectWithValue }) => {
        try {
            console.log(`Sending PATCH request for item ${tableItemId}, isCompleted: ${isCompleted}`);
            const response = await api.patch(
                `api/table/${tableItemId}/completed`,
                { isCompleted },
                { withCredentials: true }
            );
            console.log("Response from server:", response.data);
            return response.data;
        } catch (err: any) {
            console.error("Error marking item as completed:", err);
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
);

export const createTableItem = createAsyncThunk<
    ITableItemCreate,
    ITableItemCreate,
    { rejectValue: IValidationErrors }
>(
    "table/create-table-item",
    async (tableItem, { rejectWithValue }) => {
        try {
            const response = await api.post(
                "api/table/create-table-item",
                tableItem,
                { withCredentials: true }
            );
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
);

export const editTableItem = createAsyncThunk<
    ITableItemEdit,
    ITableItemEdit,
    { rejectValue: IValidationErrors }
>(
    "table/edit-table-item",
    async (tableItem, { rejectWithValue }) => {
        try {
            const response = await api.put(
                "api/table/edit-table-item",
                tableItem,
                { withCredentials: true }
            );
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
)

export const addUserToTable = createAsyncThunk<
    void,
    IUserToTable,
    { rejectValue: IValidationErrors }
>(
    "table/add-member-to-table",
    async ({tableId, memberEmail}, {rejectWithValue}) => {
        try {
                await api.post(
                    "api/table/add-member-to-table",
                    { tableId, memberEmail },
                    { withCredentials: true }
            );
            return;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
);

export const getAllMembersInTable = createAsyncThunk<
    IUserListForTable,
    string,
    { rejectValue: IValidationErrors }
>(
    "table/get-all-members-in-table",
    async (tableId, { rejectWithValue }) => {
        try {
            const response = await api.get(
                `api/table/members/${tableId}`,
                { withCredentials: true }
            );
            return response.data.$values;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;
            return rejectWithValue(error.response.data);
        }
    }
);