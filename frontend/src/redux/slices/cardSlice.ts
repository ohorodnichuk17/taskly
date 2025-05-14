import { createSlice } from "@reduxjs/toolkit";


const cardSlice = createSlice({
    name: "cardSlice",
    initialState: {},
    reducers: {},
    extraReducers: (builder) => {
        builder

    }
});

export const cardReducer = cardSlice.reducer;