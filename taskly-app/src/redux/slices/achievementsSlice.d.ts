import { IAchievementsInitialState, INewAchievement } from "../../interfaces/achievementsInterface";
export declare const achievementsReducer: import("redux").Reducer<IAchievementsInitialState>;
export declare const setNewAchievement: import("@reduxjs/toolkit").ActionCreatorWithPayload<INewAchievement | null, "achievementsSlice/setNewAchievement">;
