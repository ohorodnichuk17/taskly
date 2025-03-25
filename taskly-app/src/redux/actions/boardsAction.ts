import { createAsyncThunk } from "@reduxjs/toolkit";
import { ICardListItem, IUsersBoard } from "../../interfaces/boardInterface";
import { IValidationErrors } from "../../interfaces/generalInterface";
import { api } from "../../axios/api";
import { AxiosError } from "axios";

export const getBoardsByUserAsync = createAsyncThunk<
    IUsersBoard[],
    void,
    { rejectValue: IValidationErrors }>(
        "board/get-boards-by-user",
        async (_, { rejectWithValue }) => {
            try {
                var response = await api.get("/api/board/get-boards-by-user",
                    { withCredentials: true }
                );

                return response.data.$values;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }


        }
    )

export const getCardsListsByBoardIdAsync = createAsyncThunk<
    ICardListItem[],
    string,
    { rejectValue: IValidationErrors }>(
        "board/get-card-list-by-board-id",
        async (boardId: string, { rejectWithValue }) => {
            try {
                const response = await api.get(`/api/Cards/get-card-list-by-board-id?boardId=${boardId}`,
                    {
                        withCredentials: true
                    }
                );
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }
        }
    )