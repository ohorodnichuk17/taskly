import '../../styles/general/input-message-style.scss';
export declare enum typeOfMessage {
    success = "green",
    warning = "yellow",
    error = "red"
}
interface IInputMessage {
    typeOfMessage: typeOfMessage;
    message: string;
}
export declare const InputMessage: (props: IInputMessage) => import("react/jsx-runtime").JSX.Element;
export {};
