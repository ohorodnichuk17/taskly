import '../../styles/cardComments/card-comment-component-style.scss';
interface ICardCommentComponent {
    userId?: string;
    userName?: string;
    userAvatar?: string;
    text: string;
    createdAt: Date;
    isYourComment: boolean;
}
export declare const CardCommentComponent: (props: ICardCommentComponent) => import("react/jsx-runtime").JSX.Element;
export {};
