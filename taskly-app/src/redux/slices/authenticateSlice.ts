import { IAuthenticateInitialState, IAvatar, ICustomJwtPayload, IJwtInformation, StatusEnums } from "../../interfaces/authenticateInterfaces";
import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { changePasswordAsync, checkHasUserSentRequestToChangePasswordAsync, checkTokenAsync, getAllAvatarsAsync, loginAsync, registerAsync, sendRequestToChangePasswordAsync, sendVerificationCodeAsync, verificateEmailAsync } from "../actions/authenticateAction.ts";
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
            .addCase(loginAsync.fulfilled, (state) => {
                //state.jwtInformation = decodeJWT(action.payload);
                //document.cookie = `jwt_token=${action.payload}`
                state.isLogin = true;
            })
            .addCase(loginAsync.rejected, (state, action) => {
                /*if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown";
                }*/
            })
            .addCase(checkTokenAsync.fulfilled, (state) => {
                localStorage.setItem("isLogin", "true");
                state.isLogin = true;
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
            });


    }
})

export const authenticateReducer = authenticateSlice.reducer;
export const { /*setErrorAuthenticate,*/ /*clearJwtToken*/ setEmailOfUserWhoWantToChangePassword } = authenticateSlice.actions;