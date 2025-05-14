import '../../styles/general/input-for-code-style.scss';
import { UseFormRegister } from 'react-hook-form';
import { EmailVerificationType } from '../../validation_types/types';
export interface IInputCode {
    register: UseFormRegister<EmailVerificationType>;
    lastInput: React.Dispatch<React.SetStateAction<HTMLInputElement | null>>;
}
export declare const InputForCode: (props: IInputCode) => import("react/jsx-runtime").JSX.Element;
