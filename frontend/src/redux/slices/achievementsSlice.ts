import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IAchievement, IAchievementsInitialState, INewAchievement } from "../../interfaces/achievementsInterface";
import { getAllAchievementsAsync } from "../actions/achievementsActions";

const initialState: IAchievementsInitialState = {
    achievements: null,
    newAchievement: null
}
const achievementsSlice = createSlice({
    name: "achievementsSlice",
    initialState: initialState,
    reducers: {
        setNewAchievement: (state, payload: PayloadAction<INewAchievement | null>) => {
            state.newAchievement = payload.payload;
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(getAllAchievementsAsync.fulfilled, (state, action: PayloadAction<IAchievement[]>) => {
                state.achievements = action.payload;
            });
    }
})

export const achievementsReducer = achievementsSlice.reducer;
export const { setNewAchievement } = achievementsSlice.actions;