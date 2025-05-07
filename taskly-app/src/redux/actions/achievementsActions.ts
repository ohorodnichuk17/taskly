import { createAsyncThunk } from "@reduxjs/toolkit";
import { IAchievement } from "../../interfaces/achievementsInterface";
import { IValidationErrors } from "../../interfaces/generalInterface";
import { api } from "../../axios/api";
import { AxiosError } from "axios";

export const getAllAchievementsAsync = createAsyncThunk<
    IAchievement[],
    void,
    { rejectValue: IValidationErrors }>(
        "achievements/get-all-achievements",
        async (_, { rejectWithValue }) => {
            try {
                const response = await api.get(`/api/Achievements/get-all-achievements-by-user`,
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