export interface IGeneralInitialState {
    information: IInformationAlert | null;
}
export interface IValidationErrors {
    errors: {
        code: string;
    }[];
}
export interface IAvatarForSelect {
    id: string;
    name: string;
}
export interface IInformationAlert {
    message: string;
    type: TypeOfInformation;
}
export declare enum TypeOfInformation {
    Error = 0,
    Success = 1
}
