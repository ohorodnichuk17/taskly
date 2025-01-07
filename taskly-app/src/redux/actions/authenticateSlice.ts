import { createAsyncThunk } from '@reduxjs/toolkit'
import { IValidationErrors } from '../../interfaces/generalInterface'
import { api } from '../../axios/api'
import { AxiosError } from "axios";

export const sendVerificationCode = createAsyncThunk<
    string,
    string,
    { rejectValue: IValidationErrors }>
    (
        "authenticateion/send-verification-code",
        async (email: string, { rejectWithValue }) => {
            try {
                const response = await api.post("/api/send-verification-email", {
                    email: email
                })

                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;
                return rejectWithValue(error.response.data);
            }
        }
    );