import { createAsyncThunk } from '@reduxjs/toolkit'
import { IValidationErrors } from '../../interfaces/generalInterface'
import { api } from '../../axios/api'
import { AxiosError } from "axios";
import { IAvatar, IRegisterRequest, IVerificateEmailRequest } from '../../interfaces/authenticateInterfaces';

export const sendVerificationCodeAsync = createAsyncThunk<
    string, // Тип який повертається
    string, // Тип який передається 
    { rejectValue: IValidationErrors }> // Тип помилки
    (
        "authentication/send-verification-code",
        async (email: string, { rejectWithValue }) => {
            try {
                const response = await api.post("/api/Authentication/send-verification-code", {
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

export const verificateEmailAsync = createAsyncThunk<
    string,
    IVerificateEmailRequest,
    { rejectValue: IValidationErrors }>
    (
        "authentication/verificate-email",
        async (request: IVerificateEmailRequest, { rejectWithValue }) => {
            try {
                const response = await api.post("/api/Authentication/verificate-email", {
                    email: request.email,
                    code: request.code
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
export const getAllAvatarsAsync = createAsyncThunk<
    IAvatar[],
    void,
    { rejectValue: IValidationErrors }>
    (
        "authentication/get-all-avatars",
        async (_, { rejectWithValue }) => {
            try {
                const response = await api.get("/api/Authentication/get-all-avatars");
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }
        }
    );
export const registerAsync = createAsyncThunk<
    string,
    IRegisterRequest,
    { rejectValue: IValidationErrors }
>(
    "authenticate/register",
    async (request: IRegisterRequest, { rejectWithValue }) => {
        try {
            const response = await api.post("/api/Authentication/register", {
                email: request.email,
                password: request.password,
                confirmPassword: request.confirmPassword,
                avatarId: request.avatarId
            });
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);