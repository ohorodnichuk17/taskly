
export interface IBoardInitialState {
    listOfBoards: IUsersBoard[] | null;
}

export interface IUsersBoard {
    id: string,
    name: string,
    countOfMemebers: number,
    boardTemplateName: string,
    boardTemplateColor: string
}