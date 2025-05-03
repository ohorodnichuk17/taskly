import { IUserToTable, ITable, ITableCreate, ITableEdit, ITableItem, ITableItemCreate, ITableItemEdit, IUserListForTable } from "../../interfaces/tableInterface.ts";
import { IValidationErrors } from "../../interfaces/generalInterface.ts";
export declare const getTablesByUser: import("@reduxjs/toolkit").AsyncThunk<ITable[], string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getTableById: import("@reduxjs/toolkit").AsyncThunk<ITable, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getTableItems: import("@reduxjs/toolkit").AsyncThunk<ITableItem[], string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const createTable: import("@reduxjs/toolkit").AsyncThunk<ITableCreate, {
    name: string;
    userId: string;
}, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const deleteTable: import("@reduxjs/toolkit").AsyncThunk<boolean, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const deleteTableItem: import("@reduxjs/toolkit").AsyncThunk<boolean, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const editTable: import("@reduxjs/toolkit").AsyncThunk<ITableEdit, {
    tableId: string;
    tableName: string;
}, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const markTableItemAsCompleted: import("@reduxjs/toolkit").AsyncThunk<boolean, {
    tableItemId: string;
    isCompleted: boolean;
}, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const createTableItem: import("@reduxjs/toolkit").AsyncThunk<ITableItemCreate, ITableItemCreate, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const editTableItem: import("@reduxjs/toolkit").AsyncThunk<ITableItemEdit, ITableItemEdit, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const addUserToTable: import("@reduxjs/toolkit").AsyncThunk<void, IUserToTable, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const removeUserFromTable: import("@reduxjs/toolkit").AsyncThunk<void, IUserToTable, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
export declare const getAllMembersInTable: import("@reduxjs/toolkit").AsyncThunk<IUserListForTable, string, {
    rejectValue: IValidationErrors;
    state?: unknown;
    dispatch?: import("redux-thunk").ThunkDispatch<unknown, unknown, import("redux").UnknownAction> | undefined;
    extra?: unknown;
    serializedErrorType?: unknown;
    pendingMeta?: unknown;
    fulfilledMeta?: unknown;
    rejectedMeta?: unknown;
}>;
