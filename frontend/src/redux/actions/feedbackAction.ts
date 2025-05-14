import {createAsyncThunk} from "@reduxjs/toolkit";
import {ICreateFeedback, IFeedback, IFeedbackResponse} from "../../interfaces/feedbackInterface.ts";
import {api} from "../../axios/api.ts";

export const getAllFeedbacksAsync = createAsyncThunk<
    IFeedbackResponse[],
    void,
    { rejectValue: string }
>(
    "feedback/get-all",
    async (_, {rejectWithValue}) => {
        try {
            var response = await api.get("api/feedback/get-all");
            return response.data.$values;
        } catch (err: any) {
            return rejectWithValue(err.message);
        }
    }
);

export const getFeedbackByIdAsync = createAsyncThunk<
    IFeedbackResponse,
    string,
    { rejectValue: string }
>(
    "feedback/get-by-id",
    async (feedbackId: string, {rejectWithValue}) => {
        try {
            var response = await api.get(`api/feedback/get-by-id${feedbackId}`);
            return response.data;
        } catch (err: any) {
            return rejectWithValue(err.message);
        }
    }
);

export const createFeedbackAsync = createAsyncThunk<
    IFeedback,
    ICreateFeedback,
    { rejectValue: string }
>(
    "feedback/create",
    async (feedback: ICreateFeedback, {rejectWithValue}) => {
        try {
            var response = await api.post("api/feedback/create",
                feedback,
                {withCredentials: true});
            return response.data;
        } catch (err: any) {
            return rejectWithValue(err.message);
        }
    }
);

export const deleteFeedbackAsync = createAsyncThunk<
    boolean,
    string,
    { rejectValue: string }
>(
    "feedback/delete",
    async (feedbackId: string, {rejectWithValue}) => {
        try {
            var response = await api.delete(`api/feedback/delete/${feedbackId}`,
                {withCredentials: true});
            return response.data;
        } catch (err: any) {
            return rejectWithValue(err.message);
        }
    }
);