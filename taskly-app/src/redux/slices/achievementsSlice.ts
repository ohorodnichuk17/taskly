import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IAchievement, IAchievementsInitialState } from "../../interfaces/achievementsInterface";
import { getAllAchievementsAsync } from "../actions/achievementsActions";

const initialState: IAchievementsInitialState = {
    achievements: null
}
const achievementsSlice = createSlice({
    name: "achievementsSlice",
    initialState: initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(getAllAchievementsAsync.fulfilled, (state, action: PayloadAction<IAchievement[]>) => {
                state.achievements = action.payload;
            });
    }
})

export const achievementsReducer = achievementsSlice.reducer;