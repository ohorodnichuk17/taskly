import {IFeedback, IFeedbackInitialState, IFeedbackResponse} from "../../interfaces/feedbackInterface.ts";
import {createFeedbackAsync, deleteFeedbackAsync, getAllFeedbacksAsync, getFeedbackByIdAsync} from "../actions/feedbackAction.ts";
import {createSlice, PayloadAction} from "@reduxjs/toolkit";

const initialState : IFeedbackInitialState = {
    listOfFeedbacks: null,
}

const feedbackSlice = createSlice({
    name: "feedbackSlice",
    initialState: initialState,
    reducers: {
    },
    extraReducers(builder) {
        builder
            .addCase(getAllFeedbacksAsync.fulfilled, (state, action: PayloadAction<IFeedbackResponse[]>) => {
                state.listOfFeedbacks = action.payload;
            })
            .addCase(getAllFeedbacksAsync.rejected, (state) => {
                state.listOfFeedbacks = null;
            })
            .addCase(getFeedbackByIdAsync.fulfilled, (state, action: PayloadAction<IFeedbackResponse>) => {
                const feedbackIndex = state.listOfFeedbacks?.findIndex((feedback) => feedback.id === action.payload.id);
                if (feedbackIndex !== undefined && feedbackIndex !== -1) {
                    state.listOfFeedbacks[feedbackIndex] = action.payload;
                }
            })
            .addCase(getFeedbackByIdAsync.rejected, (state) => {
                state.listOfFeedbacks = null;
            })
            .addCase(createFeedbackAsync.fulfilled, (state, action: PayloadAction<IFeedback>) => {
                if (state.listOfFeedbacks) {
                    state.listOfFeedbacks = [...state.listOfFeedbacks, action.payload];
                } else {
                    state.listOfFeedbacks = [action.payload];
                }
            })
            .addCase(createFeedbackAsync.rejected, (state, action) => {
                state.createFeedbackError = action.payload;
            })
            .addCase(deleteFeedbackAsync.fulfilled, (state, action: PayloadAction<string>) => {
                if (state.listOfFeedbacks) {
                    state.listOfFeedbacks = state.listOfFeedbacks.filter((feedback) => feedback.id !== action.payload);
                }
            })
            .addCase(deleteFeedbackAsync.rejected, (state, action) => {
                state.deleteFeedbackError = action.payload;
            });
    },
});

export const feedbackReducer = feedbackSlice.reducer;