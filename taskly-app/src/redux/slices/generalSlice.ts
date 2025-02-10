import { createReducer, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IGeneralInitialState, IInformationAlert } from "../../interfaces/generalInterface";

const initialState: IGeneralInitialState = {
    information: null
}

const generalSlice = createSlice({
    name: "generalInitialState",
    initialState: initialState,
    reducers: {
        addInformation(state, payload: PayloadAction<IInformationAlert>) {
            state.information = payload.payload;
        },
        clearInformation(state) {
            state.information = null;
        }
    }
});

export const generalReducer = generalSlice.reducer;

export const { addInformation, clearInformation } = generalSlice.actions;