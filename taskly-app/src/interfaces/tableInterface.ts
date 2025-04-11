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

export interface ITableItem {
    id: string,
    task: string,
    status: string,
    label: string,
    members: IUserForTable[],
    startTime: Date,
    endTime: Date
}

export interface ITableItemCreate {
    task: string,
    status: string,
    label: string,
    members: string[],
    endTime: Date,
    toDoTableId: string
}

export interface IUserForTable {
    id: string,
    email: string,
    avatar: string
}