import { IValidationErrors } from "./generalInterface.ts";
export interface ITableInitialState {
    listOfTables: ITable[] | null;
    tableItems: ITableItem[] | null;
    membersList: IUserListForTable[] | null;
<<<<<<< HEAD
<<<<<<< HEAD
=======
    createTableError?: string | null;
    createTableItemError?: string | null;
    deleteTableError?: string | null;
    deleteTableItemError?: string | null;
    editTableError?: string | null;
    editTableItemError?: string | null;
>>>>>>> d03efc386315301a4c81be8b9cc25da9c7260788
=======
    createTableError?: IValidationErrors | null | undefined;
    createTableItemError?: IValidationErrors | null | undefined;
    deleteTableError?: IValidationErrors | null | undefined;
    deleteTableItemError?: IValidationErrors | null | undefined;
    editTableError?: IValidationErrors | null | undefined;
    editTableItemError?: IValidationErrors | null | undefined;
>>>>>>> 90a66854d5ec2a3c633e05d504ede3e7560a5505
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
<<<<<<< HEAD
=======
    id: string;
>>>>>>> d03efc386315301a4c81be8b9cc25da9c7260788
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
<<<<<<< HEAD
=======
    id: string;
>>>>>>> d03efc386315301a4c81be8b9cc25da9c7260788
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
