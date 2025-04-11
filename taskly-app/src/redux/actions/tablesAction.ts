import {createAsyncThunk} from "@reduxjs/toolkit";
import {ITable, ITableItem} from "../../interfaces/tableInterface.ts";
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