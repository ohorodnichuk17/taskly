import { IAuthenticateInitialState, IAvatar } from "../../interfaces/authenticateInterfaces";
import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { getAllAvatarsAsync, registerAsync, sendVerificationCodeAsync, verificateEmailAsync } from "../actions/authenticateAction.ts";
const initialState: IAuthenticateInitialState = {
    user: null,
    verificationEmail: sessionStorage.getItem("verificationEmail"),
    verificatedEmail: sessionStorage.getItem("verificatedEmail"),
    isLogin: false,
    error: null,
    avatars: null
}

const authenticateSlice = createSlice({
    name: "authenticateSlice",
    initialState: initialState,
    reducers: {
        setErrorAuthenticate(state, error: PayloadAction<string | null>) {
            state.error = error.payload;
        },
    },
    extraReducers(builder) {
        builder
            .addCase(sendVerificationCodeAsync.fulfilled, (state, action: PayloadAction<string>) => {
                state.verificationEmail = action.payload;
                sessionStorage.setItem("verificationEmail", action.payload)
            })
            .addCase(sendVerificationCodeAsync.rejected, (state, action) => {
                if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown"
                }
            })
            .addCase(verificateEmailAsync.fulfilled, (state, action: PayloadAction<string>) => {
                state.verificatedEmail = action.payload;
                sessionStorage.setItem("verificatedEmail", action.payload);
            })
            .addCase(verificateEmailAsync.rejected, (state, action) => {
                if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown"
                }
            })
            .addCase(getAllAvatarsAsync.fulfilled, (state, action: PayloadAction<IAvatar[]>) => {
                state.avatars = action.payload;
            })
            .addCase(getAllAvatarsAsync.rejected, (state, action) => {
                if (action.payload) {
                    state.error = action.payload.errors[0].code;
                }
                else {
                    state.error = action.error.message || "Uncnown"
                }
            })
            .addCase(registerAsync.fulfilled, (state) => {
                state.verificationEmail = null;
                state.verificatedEmail = null;
                sessionStorage.removeItem("verificatedEmail");
                sessionStorage.removeItem("verificationEmail");
            })
            .addCase(registerAsync.rejected, (state, action) => {
                if (action.payload) {
                    state.error = action.payload.errors[0].code;

                }
                else {
                    state.error = action.error.message || "Uncnown"
                }
            });
    }
})

export const authenticateReducer = authenticateSlice.reducer;
export const { setErrorAuthenticate } = authenticateSlice.actions;