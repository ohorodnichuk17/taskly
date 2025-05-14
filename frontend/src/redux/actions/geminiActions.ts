import { createAsyncThunk } from "@reduxjs/toolkit";
import { ICreateCardWithAI, ICreatedCard } from "../../interfaces/cardsInterface";
import { IValidationErrors } from "../../interfaces/generalInterface";
import { api } from "../../axios/api";
import { AxiosError } from "axios";

export const generateCardsWithAIAsync = createAsyncThunk<
    ICreatedCard[],
    ICreateCardWithAI,
    { rejectValue: IValidationErrors }>(
        "gemini/generate-cards-with-ai",
        async (request: ICreateCardWithAI, { rejectWithValue }) => {
            try {
                const response = await api.post("/api/gemini/create-cards-for-task", {
                    boardId: request.boardId,
                    task: request.task
                }, {
                    withCredentials: true
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
