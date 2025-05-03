import { IFeedback, IFeedbackInitialState, IFeedbackResponse } from "../../interfaces/feedbackInterface.ts";
import { createFeedbackAsync, deleteFeedbackAsync, getAllFeedbacksAsync, getFeedbackByIdAsync } from "../actions/feedbackAction.ts";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialState: IFeedbackInitialState = {
    listOfFeedbacks: [],
    createFeedbackError: null,
    deleteFeedbackError: null,
}

const feedbackSlice = createSlice({
    name: "feedbackSlice",
    initialState: initialState,
    reducers: {},
    extraReducers(builder) {
        builder
            .addCase(getAllFeedbacksAsync.fulfilled, (state, action: PayloadAction<IFeedbackResponse[]>) => {
                state.listOfFeedbacks = action.payload;
            })
            .addCase(getAllFeedbacksAsync.rejected, (state) => {
                state.listOfFeedbacks = [];
            })
            .addCase(getFeedbackByIdAsync.fulfilled, (state, action: PayloadAction<IFeedbackResponse>) => {
                if (state.listOfFeedbacks) {
                    const feedbackIndex = state.listOfFeedbacks.findIndex((feedback) => feedback.id === action.payload.id);
                    if (feedbackIndex !== -1) {
                        state.listOfFeedbacks[feedbackIndex] = action.payload;
                    }
                }
            })
            .addCase(getFeedbackByIdAsync.rejected, (state) => {
                state.listOfFeedbacks = [];
            })
            .addCase(createFeedbackAsync.fulfilled, (state, action: PayloadAction<IFeedback>) => {
                if (state.listOfFeedbacks) {
                    state.listOfFeedbacks = [...state.listOfFeedbacks, action.payload];
                } else {
                    state.listOfFeedbacks = [action.payload];
                }
            })
            .addCase(createFeedbackAsync.rejected, (state, action) => {
                state.createFeedbackError = action.payload ?? null;
            })
            .addCase(deleteFeedbackAsync.fulfilled, (state, action: PayloadAction<boolean, string, { arg: string; requestId: string; requestStatus: "fulfilled"; }, never>) => {
                const feedbackIdToDelete = action.meta.arg;

                if (state.listOfFeedbacks && feedbackIdToDelete) {
                    state.listOfFeedbacks = state.listOfFeedbacks.filter(
                        (feedback) => feedback.id !== feedbackIdToDelete
                    );
                }
            })
            .addCase(deleteFeedbackAsync.rejected, (state, action) => {
                state.deleteFeedbackError = action.payload ?? null;
            });
    },
});

export const feedbackReducer = feedbackSlice.reducer;
