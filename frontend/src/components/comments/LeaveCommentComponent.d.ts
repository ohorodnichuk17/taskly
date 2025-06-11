import { LeaveCommentType } from "../../validation_types/types";
import '../../styles/cardComments/leave-comment-component-styel.scss';
interface ILeaveCommentComponent {
    leaveComment: (obj: LeaveCommentType) => void;
}
export declare const LeaveCommentComponent: (props: ILeaveCommentComponent) => import("react/jsx-runtime").JSX.Element;
export {};
