import { IValidationErrors } from "./generalInterface.ts";
export interface ITableInitialState {
    listOfTables: ITable[] | null;
    tableItems: ITableItem[] | null;
    membersList: IUserListForTable[] | null;
    createTableError?: IValidationErrors | null | undefined;
    createTableItemError?: IValidationErrors | null | undefined;
    deleteTableError?: IValidationErrors | null | undefined;
    deleteTableItemError?: IValidationErrors | null | undefined;
    editTableError?: IValidationErrors | null | undefined;
    editTableItemError?: IValidationErrors | null | undefined;
}
export interface ITable {
    id: string;
    name: string;
    toDoItems: ITableItem[];
    members: IUserToTable[];
}
export interface ITableCreate {
    userId: string;
    name: string;
}
export interface ITableEdit {
    id: string;
    userId: string;
    name: string;
}
export interface ITableItem {
    id: string;
    task: string;
    status: string;
    label: string;
    endTime: Date;
    isCompleted: boolean;
}
export interface ITableItemCreate {
    task: string;
    status: string;
    label: string;
    endTime: Date;
    tableId: string;
    isCompleted?: boolean;
    members?: IUserListForTable[];
}
export interface ITableItemEdit {
    id: string;
    tableIdItem: string;
    task: string;
    status: string;
    label: string;
    endTime: Date;
}
export interface IUserToTable {
    tableId: string;
    memberEmail: string;
}
export interface IUserListForTable {
    $id: string;
    email: string;
    avatarId: string;
    userName?: string;
}
