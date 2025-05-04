export interface ITableInitialState {
    listOfTables: ITable[] | null;
    tableItems: ITableItem[] | null;
    membersList: IUserListForTable[] | null;
<<<<<<< HEAD
=======
    createTableError?: string | null;
    createTableItemError?: string | null;
    deleteTableError?: string | null;
    deleteTableItemError?: string | null;
    editTableError?: string | null;
    editTableItemError?: string | null;
>>>>>>> d03efc386315301a4c81be8b9cc25da9c7260788
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
