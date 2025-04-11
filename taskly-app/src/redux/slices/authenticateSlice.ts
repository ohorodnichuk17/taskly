import {
    IAuthenticateInitialState,
    IAvatar,
    ICustomJwtPayload,
    IEditUserProfile,
    IJwtInformation,
    IUserProfile,
    StatusEnums
} from "../../interfaces/authenticateInterfaces";
import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {
    changePasswordAsync,
    checkHasUserSentRequestToChangePasswordAsync,
    checkTokenAsync, editUserProfileAsync,
    getAllAvatarsAsync,
    loginAsync,
    logoutAsync,
    registerAsync,
    sendRequestToChangePasswordAsync,
    sendVerificationCodeAsync,
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
    editUserProfile: (!localStorage.getItem("user_profile_username") ||
        !localStorage.getItem("user_profile_email") ||
        !localStorage.getItem("user_profile_avatar_id")) ?
        null
        : {
            id: localStorage.getItem("user_profile_username"),
            email: localStorage.getItem("user_profile_email"),
            avatarName: localStorage.getItem("user_profile_avatar_id")
        } as IEditUserProfile,
    verificationEmail: null,
    verificatedEmail: null,
    isLogin: localStorage.getItem("isLogin") === "true" ? true : false,
    avatars: null,
    keyToChangePassword: null,
    emailOfUserWhoWantToChangePassword: null,
    //jwtInformation: null
}



const authenticateSlice = createSlice({
    name: "authenticateSlice",
    initialState: initialState,
    reducers: {
        setEmailOfUserWhoWantToChangePassword(state, payload: PayloadAction<string | null>) {
            state.emailOfUserWhoWantToChangePassword = payload.payload;
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
                state.isLogin = false;
                state.userProfile = null;
            })
            .addCase(logoutAsync.rejected, (state, action) => {
                console.error("Logout failed:", action.payload);
            })
            .addCase(editUserProfileAsync.fulfilled, (state, action) => {
                const { username, email, avatarId } = action.payload;

                state.editUserProfile = action.payload;

                localStorage.setItem("user_profile_username", username);
                localStorage.setItem("user_profile_email", email);
                localStorage.setItem("user_profile_avatar_id", avatarId);
            })
            .addCase(editUserProfileAsync.rejected, (state, action) => {
                console.error("Edit user profile failed:", action.payload);
            });
    }
})

export const authenticateReducer = authenticateSlice.reducer;
export const { /*setErrorAuthenticate,*/ /*clearJwtToken*/ setEmailOfUserWhoWantToChangePassword } = authenticateSlice.actions;