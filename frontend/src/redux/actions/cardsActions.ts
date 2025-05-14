import { createAsyncThunk } from "@reduxjs/toolkit";
import { IValidationErrors } from "../../interfaces/generalInterface";
import { ICreateCard } from "../../interfaces/cardsInterface";
import { AxiosError } from "axios";
import { api } from "../../axios/api";


export const createCardAsync = createAsyncThunk<
    string,
    ICreateCard,
    { rejectValue: IValidationErrors }>(
        "card/create-card",
        async (request: ICreateCard, { rejectWithValue }) => {
            try {
                const response = await api.post("/api/Cards/create-card", {
                    cardListId: request.cardListId,
                    task: request.task,
                    deadline: request.deadline,
                    userId: request.userId
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