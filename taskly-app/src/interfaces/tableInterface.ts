export interface ITableInitialState {
    listOfTables: ITable[] | null,
    tableItems: ITableItem[] | null
}

export interface ITable {
    id: string,
    name: string,
    toDoItems: ITableItem[],
    members: IUserForTable[]
}

export interface ITableCreate {
    userId: string,
    name: string
}

export interface ITableEdit {
    userId: string,
    name: string
}

export interface ITableItem {
    id: string,
    task: string,
    status: string,
    label: string,
    endTime: Date,
    isCompleted: boolean,
}

export interface ITableItemCreate {
    task: string,
    status: string,
    label: string,
    endTime: Date,
    tableId: string
}

export interface ITableItemEdit {
    tableIdItem: string
    task: string,
    status: string,
    label: string,
    endTime: Date,
}

export interface IAddUserToTable {
    tableId: string,
    memberEmail: string
}

export interface IUserForTable {
    id: string,
    email: string,
    avatar: string
}