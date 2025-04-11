import {createAsyncThunk} from "@reduxjs/toolkit";
import {ITable, ITableCreate, ITableItem} from "../../interfaces/tableInterface.ts";
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

export const getTableItems = createAsyncThunk<
    ITableItem,
    string,
    {rejectValue: IValidationErrors}> (
    "table/get-all-table-items",
        async (tableId, {rejectWithValue}) => {
            try {
                const response = await api.get(`api/table/get-all-table-items/${tableId}`,
                    {withCredentials: true}
                );
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if(!error.response)
                    throw err;
                return rejectWithValue(error.response.data);
            }
        }
)

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
