import { IGeneralInitialState, IInformationAlert } from "../../interfaces/generalInterface";
export declare const generalReducer: import("redux").Reducer<IGeneralInitialState>;
export declare const addInformation: import("@reduxjs/toolkit").ActionCreatorWithPayload<IInformationAlert, "generalInitialState/addInformation">, clearInformation: import("@reduxjs/toolkit").ActionCreatorWithoutPayload<"generalInitialState/clearInformation">;
