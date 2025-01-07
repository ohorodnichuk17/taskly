import { IAuthenticateInitialState } from "../../interfaces/authenticateInterfaces";
import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { sendVerificationCode } from "../actions/authenticateSlice.ts";
const initialState: IAuthenticateInitialState = {
    user: null,
    verificationEmail: null,
    isLogin: false,
    error: null
}

const authenticateSlice = createSlice({
    name: "authenticateSlice",
    initialState: initialState,
    reducers: {},
    extraReducers(builder) {
        builder
            .addCase(sendVerificationCode.fulfilled, (state, action: PayloadAction<string>) => {
                state.verificationEmail = action.payload;
            })
            .addCase(sendVerificationCode.rejected, (state, action) => {
                if (action.payload) {
                    state.error = action.payload.message;
                }
                else {
                    state.error = action.error.message || "Uncnown"
                }
            });
    }
})

export const authenticateReducer = authenticateSlice.reducer;