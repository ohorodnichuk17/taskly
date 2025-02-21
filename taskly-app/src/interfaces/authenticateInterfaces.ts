import { JwtPayload } from "jwt-decode"
import { IInformationAlert } from "./generalInterface"
export enum StatusEnums { Loading, None }
export interface IAuthenticateInitialState {
    user: IUser | null,
    userProfile: IUserProfile | null,
    verificationEmail: string | null,
    verificatedEmail: string | null,
    isLogin: boolean,
    //error: string | null,
    avatars: IAvatar[] | null,
    keyToChangePassword: string | null,
    emailOfUserWhoWantToChangePassword: string | null,

    //jwtInformation: IJwtInformation | null
}
export interface IUserProfile {
    id: string
    email: string,
    avatarName: string
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
export interface IJwtInformation {
    id: string,
    email: string,
    startTime: string,
    endTime: string
}
export interface ICustomJwtPayload extends JwtPayload {
    id: string,
    email: string
}
export interface ILoginRequest {
    email: string,
    password: string,
    rememberMe: boolean
}
export interface ICheckHasUserSentRequestToChangePassword {
    key: string
}
export interface IChangePasswordRequest {
    email: string,
    password: string,
    confirmPassword: string
}