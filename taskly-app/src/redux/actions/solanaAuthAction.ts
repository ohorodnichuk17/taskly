import {createAsyncThunk} from "@reduxjs/toolkit";
import {api} from "../../axios/api";

export const solanaWalletLoginAsync = createAsyncThunk(
    "api/solana-auth/authenticate",
    async (publicKey, {rejectWithValue}) => {
        try {
            const response = await api.post("/api/solana-auth/authenticate", {
                PublicKey: publicKey
            });
            return response.data.Token;
        } catch (error) {
            return rejectWithValue(error.response?.data || 'Authentication failed');
        }
    }
);