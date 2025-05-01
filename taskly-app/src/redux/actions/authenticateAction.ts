import { createAsyncThunk } from '@reduxjs/toolkit'
import { IValidationErrors } from '../../interfaces/generalInterface'
import { api } from '../../axios/api'
import { AxiosError } from "axios";
import {
    IAvatar,
    IChangePasswordRequest,
    ICheckHasUserSentRequestToChangePassword, IEditAvatar,
    ILoginRequest,
    IRegisterRequest, ISetUserNameForSolanaUser, ISolanaUserProfile,
    IUserProfile,
    IVerificateEmailRequest
} from '../../interfaces/authenticateInterfaces';

export const sendVerificationCodeAsync = createAsyncThunk<
    string,
    string,
    { rejectValue: IValidationErrors }>
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
    void,
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

export const loginAsync = createAsyncThunk<
    IUserProfile,
    ILoginRequest,
    { rejectValue: IValidationErrors }
>(
    "authenticate/login",
    async (request: ILoginRequest, { rejectWithValue }) => {
        try {
            var response = await api.post("/api/Authentication/login", {
                    email: request.email,
                    password: request.password,
                    rememberMe: request.rememberMe,
                },
                {
                    withCredentials: true
                });
            console.log('cookies -> ', document.cookie);
            return response.data;

        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const checkTokenAsync = createAsyncThunk<
    IUserProfile,
    void,
    { rejectValue: IValidationErrors }>(
    "authentication/check-token",
    async (_, { rejectWithValue }) => {
        try {
            var result = await api.get("api/authentication/check-token", {
                withCredentials: true // Дозволяє надсилати кукі разом з запитом
            });
            return result.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;
            return rejectWithValue(error.response.data);
        }
    }
);

export const sendRequestToChangePasswordAsync = createAsyncThunk<
    string,
    string,
    { rejectValue: IValidationErrors }
>(
    "authentication/send-request-to-change-password",
    async (email: string, { rejectWithValue }) => {
        try {
            var request = await api.post("api/authentication/send-request-to-change-password", {
                email: email
            }, {
                withCredentials: true
            });
            return request.data;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;
            return rejectWithValue(error.response.data);
        }
    }
)

export const checkHasUserSentRequestToChangePasswordAsync = createAsyncThunk<
    string | null,
    ICheckHasUserSentRequestToChangePassword,
    { rejectValue: IValidationErrors }>(
    "authentication/check-has-user-sent-request-to-change-password",
    async (request: ICheckHasUserSentRequestToChangePassword, { rejectWithValue }) => {
        try {
            const response = await api.get(`api/authentication/check-has-user-sent-request-to-change-password?Key=${request.key}`);
            console.log("response - ", response.data)
            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
)

export const editAvatarAsync = createAsyncThunk<
    {avatarId: string},
    IEditAvatar,
    { rejectValue: IValidationErrors }>(
    "authentication/edit-user-profile",
    async (request: IEditAvatar, { rejectWithValue }) => {
        try {
            const response = await api.put("api/authentication/edit-avatar", {
                userId: request.userId,
                avatarId: request.avatarId
            }, {
                withCredentials: true
            });

            console.log("RESPONSE DATA", response.data);

            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            console.log("Validation error:", error.response?.data);
            if (error.response) {
                console.error("API Error:", error.response.status, error.response.data);
            }
            return rejectWithValue(error.response?.data);
        }
    }
);

export const changePasswordAsync = createAsyncThunk<
    string,
    IChangePasswordRequest,
    { rejectValue: IValidationErrors }>(
    "authentication/change-password",
    async (request: IChangePasswordRequest, { rejectWithValue }) => {
        try {
            var response = await api.put("api/authentication/change-password", {
                email: request.email,
                password: request.password,
                confirmPassword: request.confirmPassword
            });

            return response.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;
            return rejectWithValue(error.response.data);
        }
    }
)

export const logoutAsync = createAsyncThunk<
    void,
    void,
    { rejectValue: IValidationErrors }
>(
    "authenticate/logout",
    async (_, { rejectWithValue }) => {
        try {
            await api.get("/api/Authentication/exit", {
                withCredentials: true,
            });

            localStorage.removeItem("token");

            return;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const solanaLogoutAsync = createAsyncThunk<
    void,
    void,
    { rejectValue: IValidationErrors }
>(
    "authenticate/solana-logout",
    async (_, { rejectWithValue }) => {
        try {
            await api.get("/api/Authentication/exit", {
                withCredentials: true,
            });

            localStorage.removeItem("token");

            return;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response) throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const solanaWalletAuthAsync = createAsyncThunk<
    ISolanaUserProfile,
    string,
    { rejectValue: IValidationErrors }
>(
    "api/authentication/solana-auth",
    async (publicKey, {rejectWithValue}) => {
        try {
            const response = await api.post("/api/authentication/solana-auth", {
                    PublicKey: publicKey
                },
                {withCredentials: true}
            );
            return response.data;
        } catch (error) {
            return rejectWithValue(error.response?.data || 'Authentication failed');
        }
    }
);

export const setUserNameForSolanaUserAsync = createAsyncThunk<
    string,
    ISetUserNameForSolanaUser,
    { rejectValue: IValidationErrors }
>(
    "api/authentication/set-user-name-for-solana-user",
    async (request: ISetUserNameForSolanaUser, {rejectWithValue}) => {
        try {
            const response = await api.post("/api/authentication/set-user-name-for-solana-user", {
                    PublicKey: request.publicKey,
                    UserName: request.userName
                },
                {withCredentials: true}
            );
            return response.data;
        } catch (error) {
            return rejectWithValue(error.response?.data || 'Authentication failed');
        }
    }
);

export const checkSolanaTokenAsync = createAsyncThunk<
    ISolanaUserProfile,
    void,
    { rejectValue: IValidationErrors }>(
    "authentication/check-token-by-publickey",
    async (_, { rejectWithValue }) => {
        try {
            var result = await api.get("api/authentication/check-token-by-publickey", {
                withCredentials: true
            });
            return result.data;
        } catch (err: any) {
            let error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;
            return rejectWithValue(error.response.data);
        }
    }
);