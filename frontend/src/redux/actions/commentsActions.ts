import { createAsyncThunk } from "@reduxjs/toolkit";
import { ICardComment } from "../../interfaces/cardCommentsInterface";
import { api } from "../../axios/api";
import { AxiosError } from "axios";
import { IValidationErrors } from "../../interfaces/generalInterface";

export const getCommentsByCardIdAsync = createAsyncThunk<
    ICardComment[],
    string,
    { rejectValue: IValidationErrors }>(
        "card-comments/get-comments-by-card-id",
        async (cardId: string, { rejectWithValue }) => {
            try {
                var response = await api.get(`/api/CardComments/get-card-comments-by-card-id-${cardId}`, {
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