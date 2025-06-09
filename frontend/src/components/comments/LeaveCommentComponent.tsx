import { Controller, useForm } from "react-hook-form"
import { TaskTextArea } from "../general/TaskTextArea"
import { LeaveCommentShema, LeaveCommentType } from "../../validation_types/types";
import { zodResolver } from "@hookform/resolvers/zod";
import '../../styles/cardComments/leave-comment-component-styel.scss';
import paper_airplane_icon from '../../assets/icon/paper_airplane_icon.png';
import { useState } from "react";

interface ILeaveCommentComponent {
    leaveComment: (obj: LeaveCommentType) => void
}
export const LeaveCommentComponent = (props: ILeaveCommentComponent) => {

    const {
        handleSubmit,
        control,
    } = useForm<LeaveCommentType>({
        resolver: zodResolver(LeaveCommentShema),
        defaultValues: {
            comment: ""
        }
    });

    const [commentLength, setCommentLength] = useState<number>(0);


    return (<form className="leave-comment-component"
        onSubmit={handleSubmit(props.leaveComment)}>
        <Controller
            name="comment"
            control={control}
            render={({ field }) => (
                <TaskTextArea
                    maxLength={300}
                    register={{
                        name: field.name,
                        onChange: field.onChange,
                        onBlur: field.onBlur,
                        ref: field.ref,
                    }}
                    placeholder="Comment..."
                    currentLength={setCommentLength}
                />
            )}
        />
        <button
            type="submit"
            disabled={commentLength <= 0}
        >
            <img src={paper_airplane_icon} alt="Leave comment" />
            <p>Send</p></button>
    </form>)
}