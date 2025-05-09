import {createAsyncThunk} from "@reduxjs/toolkit";
import {
    IBadgeResponse,
    IChallengeResponse,
    ICreateChallengeRequest,
    ICreateChallengeResponse, IUserBadgeResponse
} from "../../interfaces/gamificationInterface.ts";
import {AxiosError} from "axios";
import {IValidationErrors} from "../../interfaces/generalInterface.ts";
import {api} from "../../axios/api.ts";

export const createChallengeAsync = createAsyncThunk<
    ICreateChallengeResponse,
    ICreateChallengeRequest,
    { rejectValue: IValidationErrors }
>(
    "gamification/create-challenge",
    async (request, {rejectWithValue}) => {
        try {
            const response = await api.post("api/gamification/create-challenge",
                request,
                {withCredentials: true}
            );
            return response.data as ICreateChallengeResponse;
        }  catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const deleteChallengeAsync = createAsyncThunk<
    boolean,
    string,
    { rejectValue: IValidationErrors }
>(
    "gamification/delete-challenge",
    async (challengeId, {rejectWithValue}) => {
        try {
            const response = await api.delete(`api/gamification/delete-challenge/${challengeId}`,
                {withCredentials: true}
            );
            return response.data as boolean;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const getAllChallengesAsync = createAsyncThunk<
    IChallengeResponse[],
    void,
    { rejectValue: IValidationErrors }
>(
    "gamification/get-all-challenges",
    async (_, {rejectWithValue}) => {
        try {
            const response = await api.get("api/gamification/get-challenges",
                {withCredentials: true}
            );
            return response.data.$values as IChallengeResponse[];
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const getActiveChallengesAsync = createAsyncThunk<
    IChallengeResponse[],
    void,
    { rejectValue: IValidationErrors }
>(
    "gamification/get-active-challenges",
    async (_, {rejectWithValue}) => {
        try {
            const response = await api.get("api/gamification/get-active-challenges",
                {withCredentials: true}
            );
            return response.data as IChallengeResponse[];
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const getAvailableChallengesAsync = createAsyncThunk<
    IChallengeResponse[],
    void,
    { rejectValue: IValidationErrors }
>(
    "gamification/get-available-challenges",
    async (_, {rejectWithValue}) => {
        try {
            const response = await api.get("api/gamification/get-available-challenges",
                {withCredentials: true}
            );
            return response.data as IChallengeResponse[];
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const getChallengeByIdAsync = createAsyncThunk<
    IChallengeResponse,
    string,
    { rejectValue: IValidationErrors }
>(
    "gamification/get-challenge-by-id",
    async (challengeId, {rejectWithValue}) => {
        try {
            const response = await api.get(`api/gamification/get-challenge-by-id/${challengeId}`,
                {withCredentials: true}
            );
            return response.data as IChallengeResponse;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const bookChallengeAsCompletedAsync = createAsyncThunk<
    boolean,
    { challengeId: string, userId: string },
    { rejectValue: IValidationErrors }
>(
    "gamification/book-challenge",
    async ({challengeId, userId}, {rejectWithValue}) => {
        try {
            const response = await api.put(`api/gamification/book-challenge/${challengeId}/${userId}`,
                {withCredentials: true}
            );
            return response.data as boolean;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const markChallengeAsCompletedAsync = createAsyncThunk<
    boolean,
    string,
    { rejectValue: IValidationErrors }
>(
    "gamification/mark-challenge-as-completed",
    async (challengeId, {rejectWithValue}) => {
        try {
            const response = await api.put(`api/gamification/mark-challenge-as-completed/${challengeId}`,
                {},
                {withCredentials: true}
            );
            return response.data as boolean;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const getAllBadgesAsync = createAsyncThunk<
    IBadgeResponse[],
    void,
    { rejectValue: IValidationErrors }
>(
    "gamification/get-all-badges",
    async (_, {rejectWithValue}) => {
        try {
            const response = await api.get("api/gamification/get-all-badges",
                {withCredentials: true}
            );
            return response.data as IBadgeResponse[];
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const getBadgeByIdAsync = createAsyncThunk<
    IBadgeResponse,
    string,
    { rejectValue: IValidationErrors }
>(
    "gamification/get-badge-by-id",
    async (id, {rejectWithValue}) => {
        try {
            const response = await api.get(`api/gamification/get-badge-by-id/${id}`,
                {withCredentials: true}
            );
            return response.data as IBadgeResponse;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const getUserLevelAsync = createAsyncThunk<
    number,
    string,
    { rejectValue: IValidationErrors }
>(
    "gamification/get-user-level",
    async (userId, {rejectWithValue}) => {
        try {
            const response = await api.get(`api/gamification/get-user-level/${userId}`,
                {withCredentials: true}
            );
            return response.data as number;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const getAllBadgesByUserIdAsync = createAsyncThunk<
    IUserBadgeResponse[],
    string,
    { rejectValue: IValidationErrors }
>(
    "gamification/get-all-badges-by-user-id",
    async (userId, {rejectWithValue}) => {
        try {
            const response = await api.get(`api/gamification/get-all-badges-by-user-id/${userId}`,
                {withCredentials: true}
            );
            return response.data as IUserBadgeResponse[];
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);

export const getUserBadgeByUserIdAndBadgeIdAsync = createAsyncThunk<
    IUserBadgeResponse,
    { userId: string, badgeId: string },
    { rejectValue: IValidationErrors }
>(
    "gamification/get-user-badge-by-user-id-and-badge-id",
    async ({userId, badgeId}, {rejectWithValue}) => {
        try {
            const response = await api.get(`api/gamification/get-user-badge-by-user-id-and-badge-id/${userId}/${badgeId}`,
                {withCredentials: true}
            );
            return response.data as IUserBadgeResponse;
        } catch (err: any) {
            const error: AxiosError<IValidationErrors> = err;
            if (!error.response)
                throw err;

            return rejectWithValue(error.response.data);
        }
    }
);