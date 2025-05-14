import '../../styles/general/input-message-style.scss';

export enum typeOfMessage { success = "green", warning = "yellow", error = "red" }
interface IInputMessage {
    typeOfMessage: typeOfMessage
    message: string
}
export const InputMessage = (props: IInputMessage) => {
    return (
        <p className='input-message' style={{ color: props.typeOfMessage }}>
            {props.message}
        </p>
    )
}