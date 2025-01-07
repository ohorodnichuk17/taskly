export interface IAuthenticateInitialState {
    user: IUser | null,
    verificationEmail: string | null,
    isLogin: boolean,
    error: string | null
}
export interface IUser {
    id: string
    email: string,
    boardId: string
}