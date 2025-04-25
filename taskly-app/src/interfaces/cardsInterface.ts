export interface ICreateCard {
    cardListId: string,
    task: string,
    deadline: Date,
    userId: string | null
}