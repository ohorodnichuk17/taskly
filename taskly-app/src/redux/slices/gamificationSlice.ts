import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {
    createChallengeAsync,
    deleteChallengeAsync,
    getAllChallengesAsync,
    getActiveChallengesAsync,
    getAvailableChallengesAsync,
    getChallengeByIdAsync,
    bookChallengeAsCompletedAsync,
    markChallengeAsCompletedAsync,
    getAllBadgesAsync,
    getBadgeByIdAsync,
    getUserLevelAsync,
    getAllBadgesByUserIdAsync,
    getUserBadgeByUserIdAndBadgeIdAsync,
} from "../actions/gamificationAction.ts";
import {
    IGamificationInitialState,
    IChallengeResponse,
    IBadgeResponse,
    IUserBadgeResponse,
} from "../../interfaces/gamificationInterface.ts";

const initialState: IGamificationInitialState = {
    challenges: null,
    availableChallenges: null,
    activeChallenges: null,
    badges: null,
    userBadges: null,
    isLoadingChallenges: false,
    isLoadingBadges: false,
    isLoadingUserBadges: false,
    isCreatingChallenge: false,
    isDeletingChallenge: false,
    error: null,
};

const gamificationSlice = createSlice({
    name: "gamification",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(getAllChallengesAsync.pending, (state) => {
            state.isLoadingChallenges = true;
            state.error = null;
        });
        builder.addCase(getAllChallengesAsync.fulfilled, (state, action: PayloadAction<IChallengeResponse[]>) => {
            state.challenges = action.payload;
            state.isLoadingChallenges = false;
        });
        builder.addCase(getAllChallengesAsync.rejected, (state, action) => {
            state.isLoadingChallenges = false;
            state.error = typeof action.payload === "object" && action.payload && "message" in action.payload
                ? (action.payload as any).message
                : "Failed to fetch challenges.";
        });
        builder.addCase(getActiveChallengesAsync.pending, (state) => {
            state.isLoadingChallenges = true;
            state.error = null;
        });
        builder.addCase(getActiveChallengesAsync.fulfilled, (state, action: PayloadAction<IChallengeResponse[]>) => {
            state.activeChallenges = action.payload;
            state.isLoadingChallenges = false;
        });
        builder.addCase(getActiveChallengesAsync.rejected, (state, action) => {
            state.isLoadingChallenges = false;
            state.error = typeof action.payload === "object" && action.payload && "message" in action.payload
                ? (action.payload as any).message
                : "Failed to active challenges.";
        });
        builder.addCase(getAvailableChallengesAsync.pending, (state) => {
            state.isLoadingChallenges = true;
            state.error = null;
        });
        builder.addCase(getAvailableChallengesAsync.fulfilled, (state, action: PayloadAction<IChallengeResponse[]>) => {
            state.availableChallenges = action.payload;
            state.isLoadingChallenges = false;
        });
        builder.addCase(getAvailableChallengesAsync.rejected, (state, action) => {
            state.isLoadingChallenges = false;
            state.error = typeof action.payload === "object" && action.payload && "message" in action.payload
                ? (action.payload as any).message
                : "Failed to fetch available challenges.";
        });
        builder.addCase(getChallengeByIdAsync.fulfilled, (state, action) => {
            const challenge = action.payload as IChallengeResponse;
            if (!state.challenges) {
                state.challenges = [challenge];
            } else {
                const index = state.challenges.findIndex(
                    (c) => c.id === challenge.id
                );
                if (index !== -1) {
                    state.challenges[index] = challenge;
                } else {
                    state.challenges.push(challenge);
                }
            }
        });
        builder.addCase(createChallengeAsync.pending, (state) => {
            state.isCreatingChallenge = true;
            state.error = null;
        });
        builder.addCase(createChallengeAsync.fulfilled, (state, action) => {
            const newChallenge = action.payload as IChallengeResponse;
            if (state.challenges === null) {
                state.challenges = [newChallenge];
            } else {
                state.challenges.push(newChallenge);
            }
            state.isCreatingChallenge = false;
        });
        builder.addCase(createChallengeAsync.rejected, (state, action) => {
            state.isCreatingChallenge = false;
            state.error = typeof action.payload === "object" && action.payload && "message" in action.payload
                ? (action.payload as any).message
                : "Failed to create challenge.";
        });
        builder.addCase(deleteChallengeAsync.pending, (state) => {
            state.isDeletingChallenge = true;
            state.error = null;
        });
        builder.addCase(deleteChallengeAsync.fulfilled, (state, action) => {
            if (action.payload && state.challenges) {
                state.challenges = state.challenges.filter(
                    (challenge) => challenge.id !== action.meta.arg
                );
            }
            state.isDeletingChallenge = false;
        });
        builder.addCase(deleteChallengeAsync.rejected, (state, action) => {
            state.isDeletingChallenge = false;
            state.error = typeof action.payload === "object" && action.payload && "message" in action.payload
                ? (action.payload as any).message
                : "Failed to delete challenge.";
        });
        builder.addCase(markChallengeAsCompletedAsync.fulfilled, (state, action) => {
            if (!state.challenges) return;
            const challenge = state.challenges.find((c) => c.id === action.meta.arg);
            if (challenge) {
                challenge.isCompleted = true;
            }
        });
        builder.addCase(bookChallengeAsCompletedAsync.fulfilled, (state, action) => {
            const { challengeId } = action.meta.arg;
            const challenge = state.challenges?.find((c) => c.id === challengeId);
            if (challenge) {
                challenge.isBooked = true;
            }
        });
        builder.addCase(getAllBadgesAsync.pending, (state) => {
            state.isLoadingBadges = true;
            state.error = null;
        });
        builder.addCase(getAllBadgesAsync.fulfilled, (state, action: PayloadAction<IBadgeResponse[]>) => {
            state.badges = action.payload;
            state.isLoadingBadges = false;
        });
        builder.addCase(getAllBadgesAsync.rejected, (state, action) => {
            state.isLoadingBadges = false;
            state.error = typeof action.payload === "object" && action.payload && "message" in action.payload
                ? (action.payload as any).message
                : "Failed to fetch badges.";
        });
        builder.addCase(getBadgeByIdAsync.fulfilled, (state, action) => {
            const badge = action.payload as IBadgeResponse;
            if (!state.badges) {
                state.badges = [badge];
            } else {
                const index = state.badges.findIndex((b) => b.id === badge.id);
                if (index !== -1) {
                    state.badges[index] = badge;
                } else {
                    state.badges.push(badge);
                }
            }
        });
        builder.addCase(getUserLevelAsync.fulfilled, () => {});
        builder.addCase(getAllBadgesByUserIdAsync.pending, (state) => {
            state.isLoadingUserBadges = true;
            state.error = null;
        });
        builder.addCase(getAllBadgesByUserIdAsync.fulfilled, (state, action: PayloadAction<IUserBadgeResponse[]>) => {
            state.userBadges = action.payload;
        });
        builder.addCase(getAllBadgesByUserIdAsync.rejected, (state, action) => {
            state.isLoadingUserBadges = false;
            state.error = typeof action.payload === "object" && action.payload && "message" in action.payload
                ? (action.payload as any).message
                : "Failed to fetch badges by user.";
        });
        builder.addCase(getUserBadgeByUserIdAndBadgeIdAsync.fulfilled, (state, action) => {
            const userBadge = action.payload as IUserBadgeResponse;
            if (!state.userBadges) {
                state.userBadges = [userBadge];
            } else {
                const index = state.userBadges.findIndex(
                    (ub) => ub.badge.name === userBadge.badge.name
                );
                if (index !== -1) {
                    state.userBadges[index] = userBadge;
                } else {
                    state.userBadges.push(userBadge);
                }
            }
        });
    },
});

export const gamificationReducer = gamificationSlice.reducer;