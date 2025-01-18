export interface IAuthenticateInitialState {
    user: IUser | null,
    verificationEmail: string | null,
    verificatedEmail: string | null,
    isLogin: boolean,
    error: string | null,
    avatars: IAvatar[] | null
}
export interface IUser {
    id: string
    email: string,
    boardId: string
}
export interface IVerificateEmailRequest {
    email: string,
    code: string
}
export interface IAvatar {
    id: string,
    name: string
}
export interface IRegisterRequest {
    email: string,
    password: string,
    confirmPassword: string,
    avatarId: string
}