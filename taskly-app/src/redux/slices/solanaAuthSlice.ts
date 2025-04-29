import {createSlice} from "@reduxjs/toolkit";
import {solanaWalletLoginAsync} from "../actions/solanaAuthAction.ts";

const solanaAuthSlice = createSlice({
    name: 'auth',
    initialState: {
        token: null,
        loading: false,
        error: null,
        isAuthenticated: false,
    },
    reducers: {
        logout: (state) => {
            state.token = null;
            state.isAuthenticated = false;
            localStorage.removeItem('authToken');
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(solanaWalletLoginAsync.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(solanaWalletLoginAsync.fulfilled, (state, action) => {
                state.loading = false;
                state.token = action.payload;
                state.isAuthenticated = true;
                localStorage.setItem('authToken', action.payload);
            })
            .addCase(solanaWalletLoginAsync.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload || 'Something went wrong';
            });
    },
});

export const { logout } = solanaAuthSlice.actions;
export const solanaAuthReducer = solanaAuthSlice.reducer;
