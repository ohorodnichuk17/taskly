import {
    IAuthenticateInitialState,
    IAvatar,
    ICustomJwtPayload, IEditAvatar,
    IEditUserProfile,
    IJwtInformation, ISolanaUserProfile,
    IUserProfile,
    StatusEnums
} from "../../interfaces/authenticateInterfaces";
import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {
    changePasswordAsync,
    checkHasUserSentRequestToChangePasswordAsync, checkSolanaTokenAsync,
    checkTokenAsync, editAvatarAsync, editUserProfileAsync,
    getAllAvatarsAsync,
    loginAsync,
    logoutAsync,
    registerAsync,
    sendRequestToChangePasswordAsync,
    sendVerificationCodeAsync, solanaWalletAuthAsync,
    verificateEmailAsync
} from "../actions/authenticateAction.ts";
import { jwtDecode } from "jwt-decode";
import { IInformationAlert } from "../../interfaces/generalInterface.ts";


const decodeJWT = (token: string) => {
    const decode = jwtDecode(token) as ICustomJwtPayload;

    return {
        id: decode.id,
        email: decode.email,
        startTime: new Date(decode.nbf! * 1000).toISOString(),
        endTime: new Date(decode.exp! * 1000).toISOString()
    } as IJwtInformation

}
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
        !localStorage.getItem("user_profile_avatarId")) ?
        null
        : {
            id: localStorage.getItem("user_profile_id"),
            publicKey: localStorage.getItem("user_profile_publicKey"),
            avatarId: localStorage.getItem("user_profile_avatarId")
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
    isAuthenticated: false,
    //jwtInformation: null
}

const authenticateSlice = createSlice({
    name: "authenticateSlice",
    initialState: initialState,
    reducers: {
        setEmailOfUserWhoWantToChangePassword(state, payload: PayloadAction<string | null>) {
            state.emailOfUserWhoWantToChangePassword = payload.payload;
        },
        logout: (state) => {
            state.authMethod = null;
            localStorage.removeItem("authMethod");
            state.token = null;
            state.isAuthenticated = false;
            localStorage.removeItem('authToken');
        },

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
            .addCase(sendVerificationCodeAsync.rejected, (state, action) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown"
                }*/
            })
            .addCase(verificateEmailAsync.fulfilled, (state, action: PayloadAction<string>) => {
                state.verificatedEmail = action.payload;
            })
            .addCase(verificateEmailAsync.rejected, (state, action) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown"
                }*/
            })
            .addCase(getAllAvatarsAsync.fulfilled, (state, action: PayloadAction<IAvatar[]>) => {
                state.avatars = action.payload;
            })
            .addCase(getAllAvatarsAsync.rejected, (state, action) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown"
                }*/
            })
            .addCase(registerAsync.fulfilled, (state) => {
                state.verificationEmail = null;
                state.verificatedEmail = null;
                /*state.jwtInformation = decodeJWT(action.payload);*/
                state.isLogin = true;
            })
            .addCase(registerAsync.rejected, (state, action) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;

                }
                else {
                    state.error = action.error.message || "Uncnown";
                }*/
            })
            .addCase(loginAsync.fulfilled, (state, action: PayloadAction<IUserProfile>) => {
                //state.jwtInformation = decodeJWT(action.payload);
                //document.cookie = `jwt_token=${action.payload}`
                state.isLogin = true;
                state.userProfile = action.payload;
                state.authMethod = "jwt";
                localStorage.setItem("authMethod", "jwt");
                localStorage.setItem("user_profile_id", action.payload.id);
                localStorage.setItem("user_profile_email", action.payload.email);
                localStorage.setItem("user_profile_avatar", action.payload.avatarName);
            })
            .addCase(loginAsync.rejected, (state, action) => {
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
                if (localStorage.getItem("user_profile_id") !== null)
                    localStorage.removeItem("user_profile_id");
                if (localStorage.getItem("user_profile_email") !== null)
                    localStorage.removeItem("user_profile_email");
                if (localStorage.getItem("user_profile_avatar") !== null)
                    localStorage.removeItem("user_profile_avatar");
            })
            .addCase(sendRequestToChangePasswordAsync.fulfilled, (state, action: PayloadAction<string>) => {
                //state.keyToChangePassword = action.payload;
                //state.keyToChangePassword = document.cookie;
            })
            .addCase(sendRequestToChangePasswordAsync.rejected, (state) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown";
                }*/
            }).addCase(checkHasUserSentRequestToChangePasswordAsync.fulfilled, (state, action: PayloadAction<string | null>) => {
                state.emailOfUserWhoWantToChangePassword = action.payload === "" ? null : action.payload;
            })
            .addCase(checkHasUserSentRequestToChangePasswordAsync.rejected, (state) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown";
                }*/
            }).addCase(changePasswordAsync.fulfilled, (state, action: PayloadAction<string>) => {

            })
            .addCase(changePasswordAsync.rejected, (state) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown";
                }*/
            })
            .addCase(logoutAsync.fulfilled, (state) => {
                state.authMethod = null;
                localStorage.removeItem("authMethod");
                state.isLogin = false;
                state.userProfile = null;
            })
            .addCase(logoutAsync.rejected, (state, action) => {
                console.error("Logout failed:", action.payload);
            })
            .addCase(editAvatarAsync.fulfilled, (state, action) => {
                const { userId, avatarId } = action.payload;

                state.editAvatar = action.payload;

                localStorage.setItem("user_profile_id", userId);
                localStorage.setItem("avatar_id", avatarId);
            })
            .addCase(editAvatarAsync.rejected, (state, action) => {
                console.error("Edit avatar failed:", action.payload);
            })
            .addCase(solanaWalletAuthAsync.fulfilled, (state, action: PayloadAction<ISolanaUserProfile>) => {
                state.isLogin = true;
                state.isAuthenticated = true;
                state.token = action.payload;
                localStorage.setItem("authToken", action.payload);
                localStorage.setItem("authMethod", "solana");
                localStorage.setItem("user_profile_id", action.payload.id);
                localStorage.setItem("user_profile_publicKey", action.payload.publicKey);
                localStorage.setItem("user_profile_avatarId", action.payload.avatarId);
            })
            .addCase(solanaWalletAuthAsync.rejected, (state, action) => {
                if (localStorage.getItem("user_profile_id") !== null)
                    localStorage.removeItem("user_profile_id");
                if (localStorage.getItem("user_profile_publicKey") !== null)
                    localStorage.removeItem("user_profile_publicKey");
                if (localStorage.getItem("user_profile_avatarId") !== null)
                    localStorage.removeItem("user_profile_avatarId");
            })
            .addCase(checkSolanaTokenAsync.fulfilled, (state, action: PayloadAction<ISolanaUserProfile>) => {
                localStorage.setItem("isLogin", "true");
                state.isLogin = true;

                state.solanaUserProfile = action.payload;
                localStorage.setItem("user_profile_id", action.payload.id);
                localStorage.setItem("user_profile_publicKey", action.payload.publicKey);
                localStorage.setItem("user_profile_avatarId", action.payload.avatarId);
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
                if (localStorage.getItem("user_profile_id") !== null)
                    localStorage.removeItem("user_profile_id");
                if (localStorage.getItem("user_profile_publicKey") !== null)
                    localStorage.removeItem("user_profile_publicKey");
                if (localStorage.getItem("user_profile_avatarId") !== null)
                    localStorage.removeItem("user_profile_avatarId");
            });
    }
})

export const authenticateReducer = authenticateSlice.reducer;
export const { /*setErrorAuthenticate,*/ /*clearJwtToken*/ setEmailOfUserWhoWantToChangePassword, logout } = authenticateSlice.actions;