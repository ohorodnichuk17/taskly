export interface IGeneralInitialState {
    information: IInformationAlert | null
}

export interface IValidationErrors {
    errors: {
        code: string
    }[]
}

export interface IAvatarForSelect {
    id: string,
    name: string
}

export interface IInformationAlert {
    message: string,
    type: TypeOfInformation,
}
export enum TypeOfInformation { Error, Success };