import { baseUrl } from "../../axios/baseUrl";
import { format } from "date-fns";
import '../../styles/cardComments/card-comment-component-style.scss';

interface ICardCommentComponent {
    userId?: string,
    userName?: string,
    userAvatar?: string,
    text: string,
    createdAt: Date,
    isYourComment: boolean
}
export const CardCommentComponent = (props: ICardCommentComponent) => {
    return (<div className={`card-comment-component ${props.isYourComment === true ? "card-comment-component-right" : "card-comment-component-left"}`}>
        {props.isYourComment === false
            && props.userId && props.userAvatar && props.userId
            &&
            <div className="sender-of-comment">
                <div className="sender-of-comment-avatar">
                    <img src={`${baseUrl}/images/avatars/${props.userAvatar}.png`} alt="Avatar of sender" />
                </div>
                <div className="sender-of-comment-username">
                    {props.userName}
                </div>
            </div>
        }
        <div className="card-comment-text">
            {props.text}
        </div>
        <div className="card-comment-created-at">
            {format(props.createdAt, "HH:mm")}
        </div>
    </div>)
}