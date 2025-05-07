import {
    IAuthenticateInitialState,
    IAvatar,
    IEditAvatar,
    ISolanaUserProfile,
    IUserProfile,
} from "../../interfaces/authenticateInterfaces";
import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {
    checkHasUserSentRequestToChangePasswordAsync, checkSolanaTokenAsync,
    checkTokenAsync, editAvatarAsync,
    getAllAvatarsAsync,
    loginAsync,
    logoutAsync,
    registerAsync,
    sendVerificationCodeAsync, solanaLogoutAsync, solanaWalletAuthAsync,
    verificateEmailAsync
} from "../actions/authenticateAction.ts";

const initialState: IAuthenticateInitialState = {
    authMethod: localStorage.getItem("authMethod") as "jwt" | "solana" | null,
    user: null,
    userProfile: (!localStorage.getItem("user_profile_id") ||
        !localStorage.getItem("user_profile_email") ||
        !localStorage.getItem("user_profile_avatar")) ?
        null
        : {
            id: localStorage.getItem("user_profile_id"),
            email: localStorage.getItem("user_profile_email"),
            avatarName: localStorage.getItem("user_profile_avatar")
        } as IUserProfile,
    solanaUserProfile: (!localStorage.getItem("user_profile_id") ||
        !localStorage.getItem("user_profile_publicKey") ||
        !localStorage.getItem("user_profile_userName") ||
        !localStorage.getItem("user_profile_avatar")) ?
        null
        : {
            id: localStorage.getItem("user_profile_id"),
            publicKey: localStorage.getItem("user_profile_publicKey"),
            userName: localStorage.getItem("user_profile_userName"),
            avatarName: localStorage.getItem("user_profile_avatar")
        } as ISolanaUserProfile,
    editAvatar: (!localStorage.getItem("user_profile_id") ||
        !localStorage.getItem("avatar_id")) ?
        null
        : {
            userId: localStorage.getItem("user_profile_id"),
            avatarId: localStorage.getItem("avatar_id")
        } as IEditAvatar,
    verificationEmail: null,
    verificatedEmail: null,
    isLogin: localStorage.getItem("isLogin") === "true" ? true : false,
    avatars: null,
    keyToChangePassword: null,
    emailOfUserWhoWantToChangePassword: null,
    token: null,
    isAuthenticated: (!localStorage.getItem("isAuthenticated") ? false : true),
    //jwtInformation: null
}

const authenticateSlice = createSlice({
    name: "authenticateSlice",
    initialState: initialState,
    reducers: {
        setEmailOfUserWhoWantToChangePassword(state, payload: PayloadAction<string | null>) {
            state.emailOfUserWhoWantToChangePassword = payload.payload;
        },
        // logout: (state) => {
        //     state.authMethod = null;
        //     state.token = null;
        //     state.solanaUserProfile = null;
        //     state.isAuthenticated = false;
        //     localStorage.removeItem("authMethod");
        //     localStorage.removeItem("user_profile_id");
        //     localStorage.removeItem("user_profile_publicKey");
        //     localStorage.removeItem("user_profile_avatar");
        //     localStorage.removeItem("user_profile_userName");
        // },

        /*setErrorAuthenticate(state, error: PayloadAction<string | null>) {
            state.error = error.payload;
        },*/
        /*clearJwtToken(state) {
            state.jwtInformation = null;
        }*/
    },
    extraReducers(builder) {
        builder
            .addCase(sendVerificationCodeAsync.fulfilled, (state, action: PayloadAction<string>) => {
                state.verificationEmail = action.payload;
            })
            .addCase(verificateEmailAsync.fulfilled, (state, action: PayloadAction<string>) => {
                state.verificatedEmail = action.payload;
            })
            .addCase(getAllAvatarsAsync.fulfilled, (state, action: PayloadAction<IAvatar[]>) => {
                state.avatars = action.payload;
            })
            .addCase(registerAsync.fulfilled, (state) => {
                state.verificationEmail = null;
                state.verificatedEmail = null;
                state.isLogin = true;
            })
            .addCase(loginAsync.fulfilled, (state, action: PayloadAction<IUserProfile>) => {
                state.isLogin = true;
                state.userProfile = action.payload;
                state.authMethod = "jwt";
                localStorage.setItem("authMethod", "jwt");
                localStorage.setItem("user_profile_id", action.payload.id);
                localStorage.setItem("user_profile_email", action.payload.email);
                localStorage.setItem("user_profile_avatar", action.payload.avatarName);
            })
            .addCase(loginAsync.rejected, () => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown";
                }*/
                if (localStorage.getItem("user_profile_id") !== null)
                    localStorage.removeItem("user_profile_id");
                if (localStorage.getItem("user_profile_email") !== null)
                    localStorage.removeItem("user_profile_email");
                if (localStorage.getItem("user_profile_avatar") !== null)
                    localStorage.removeItem("user_profile_avatar");
            })
            .addCase(checkTokenAsync.fulfilled, (state, action: PayloadAction<IUserProfile>) => {
                localStorage.setItem("isLogin", "true");
                state.isLogin = true;

                state.userProfile = action.payload;
                localStorage.setItem("user_profile_id", action.payload.id);
                localStorage.setItem("user_profile_email", action.payload.email);
                localStorage.setItem("user_profile_avatar", action.payload.avatarName);
            })
            .addCase(checkTokenAsync.rejected, (state) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown";
                }*/
                localStorage.removeItem("isLogin");
                state.isLogin = false;
                state.isAuthenticated = false;
                if (localStorage.getItem("user_profile_id") !== null)
                    localStorage.removeItem("user_profile_id");
                if (localStorage.getItem("user_profile_email") !== null)
                    localStorage.removeItem("user_profile_email");
                if (localStorage.getItem("user_profile_avatar") !== null)
                    localStorage.removeItem("user_profile_avatar");
                if (localStorage.getItem("isAuthenticated") !== null)
                    localStorage.removeItem("isAuthenticated");
            })
            .addCase(checkHasUserSentRequestToChangePasswordAsync.fulfilled, (state, action: PayloadAction<string | null>) => {
                state.emailOfUserWhoWantToChangePassword = action.payload === "" ? null : action.payload;
            })
            .addCase(logoutAsync.fulfilled, (state) => {
                state.authMethod = null;
                localStorage.removeItem("authMethod");
                localStorage.removeItem("isLogin");
                localStorage.removeItem("userProfile");
                localStorage.removeItem("user_profile_id");
                localStorage.removeItem("user_profile_email");
                localStorage.removeItem("user_profile_avatar");
                state.isLogin = false;
                state.userProfile = null;
            })
            .addCase(logoutAsync.rejected, (_, action) => {
                console.error("Logout failed:", action.payload);
            })
            .addCase(solanaLogoutAsync.fulfilled, (state) => {
                state.isLogin = false;
                state.authMethod = null;
                state.token = null;
                state.solanaUserProfile = null;
                state.isAuthenticated = false;
                localStorage.removeItem("authMethod");
                localStorage.removeItem("user_profile_id");
                localStorage.removeItem("user_profile_publicKey");
                localStorage.removeItem("user_profile_avatar");
                localStorage.removeItem("user_profile_userName");
                localStorage.removeItem("isAuthenticated");
            })
            .addCase(solanaLogoutAsync.rejected, () => {
            })
            .addCase(editAvatarAsync.fulfilled, (state, action) => {
                const { avatarId, userId } = action.payload;
                state.editAvatar = { userId, avatarId };
                if (userId) {
                    localStorage.setItem("user_profile_id", userId);
                }
                localStorage.setItem("avatar_id", avatarId);
            })
            .addCase(editAvatarAsync.rejected, (_, action) => {
                console.error("Edit avatar failed:", action.payload);
            })
            .addCase(solanaWalletAuthAsync.fulfilled, (state, action: PayloadAction<Partial<ISolanaUserProfile>>) => {
                const payload = action.payload || {};
                state.isLogin = true;
                state.isAuthenticated = true;
                state.solanaUserProfile = {
                    id: payload.id || localStorage.getItem("user_profile_id") || "",
                    publicKey: payload.publicKey || localStorage.getItem("user_profile_publicKey") || "",
                    userName: payload.userName || localStorage.getItem("user_profile_userName") || "",
                    avatarName: payload.avatarName || localStorage.getItem("user_profile_avatar") || ""
                };
                state.authMethod = "solana";
                localStorage.setItem("authMethod", "solana");
                localStorage.setItem("isAuthenticated", "true");
            })
            .addCase(solanaWalletAuthAsync.rejected, () => {
                if (localStorage.getItem("user_profile_id") !== null)
                    localStorage.removeItem("user_profile_id");
                if (localStorage.getItem("user_profile_publicKey") !== null)
                    localStorage.removeItem("user_profile_publicKey");
                if (localStorage.getItem("user_profile_userName") !== null)
                    localStorage.removeItem("user_profile_userName");
                if (localStorage.getItem("user_profile_avatar") !== null)
                    localStorage.removeItem("user_profile_avatar");
            })
            .addCase(checkSolanaTokenAsync.fulfilled, (state, action: PayloadAction<ISolanaUserProfile>) => {
                localStorage.setItem("isLogin", "true");
                state.isLogin = true;

                state.solanaUserProfile = action.payload;
                localStorage.setItem("user_profile_id", action.payload.id);
                localStorage.setItem("user_profile_publicKey", action.payload.publicKey);
                localStorage.setItem("user_profile_userName", action.payload.publicKey);
                localStorage.setItem("user_profile_avatar", action.payload.avatarName);
            })
            .addCase(checkSolanaTokenAsync.rejected, (state) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown";
                }*/
                localStorage.removeItem("isLogin");
                state.isLogin = false;
                state.isAuthenticated = false;
                if (localStorage.getItem("user_profile_id") !== null)
                    localStorage.removeItem("user_profile_id");
                if (localStorage.getItem("user_profile_publicKey") !== null)
                    localStorage.removeItem("user_profile_publicKey");
                if (localStorage.getItem("user_profile_userName") !== null)
                    localStorage.removeItem("user_profile_userName");
                if (localStorage.getItem("user_profile_avatar") !== null)
                    localStorage.removeItem("user_profile_avatar");
                if (localStorage.getItem("isAuthenticated") !== null)
                    localStorage.removeItem("isAuthenticated");
            });
    }
})

export const authenticateReducer = authenticateSlice.reducer;
export const { /*setErrorAuthenticate,*/ /*clearJwtToken*/ setEmailOfUserWhoWantToChangePassword } = authenticateSlice.actions;