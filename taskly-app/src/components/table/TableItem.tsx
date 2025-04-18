import "../../styles/table/table-item-styles.scss";
import { useState } from "react";
import { ChromePicker } from "react-color";
import {useNavigate, useParams} from "react-router-dom";
import {addTableItem} from "../../redux/actions/tablesAction.ts";
import {useDispatch} from "react-redux";

export default function TableItem({ item }: TableItemProps) {
    const tableId = useParams();
    const [task, setTask] = useState<string>("");
    const [status, setStatus] = useState<string>("");
    const [label, setLabel] = useState<string>("");
    const [startTime, setStartTime] = useState<Date>(null);
    const [endTime, setEndTime] = useState<Date>(null);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const normalizeStatus = (status: string): string => {
        return status.trim().toLowerCase().replace(/\s+/g, "");
    };

    const [labelColor, setLabelColor] = useState<string>(() => {
        const savedColor = localStorage.getItem(`labelColor-${item.task}`);
        return savedColor || item.label || "#ffffff";
    });

    const [isColorPickerOpen, setIsColorPickerOpen] = useState(false);

    const handleLabelClick = () => {
        setIsColorPickerOpen(true);
    };

    const handleColorChange = (color: { hex: string }) => {
        const newColor = color.hex;
        setLabelColor(newColor);
        localStorage.setItem(`labelColor-${item.task}`, newColor);
    };

    const handleClosePicker = () => {
        setIsColorPickerOpen(false);
    };

    const createNewTableItem = async () => {
        if(tableId && task && status && label && startTime && endTime) {
            try {
                await dispatch(addTableItem({task, status, label, startTime, endTime, tableId}));
                navigate("/");
            } catch (err) {
                console.error("Failed to create table item:", err);
            }
        }
    }

    return (
        <>
            <div className="table-item">
                <div className="table-item-header">
                    <h4>Task</h4>
                    <h4>Status</h4>
                    <h4>Label</h4>
                    <h4>Due Date</h4>
                </div>

                <div className="table-item-content">
                    <div className="column task">{item.task}</div>
                    <div className="column status">
                    <span
                        className={`status ${
                            normalizeStatus(item.status) === "todo"
                                ? "status--todo"
                                : normalizeStatus(item.status) === "inprogress"
                                    ? "status--in-progress"
                                    : "status--done"
                        }`}
                    >
                        {item.status}
                    </span>
                    </div>
                    <div className="column label" onClick={handleLabelClick}>
                    <span
                        className="label-dot"
                        style={{
                            backgroundColor: labelColor,
                            boxShadow: `0 0 5px ${labelColor}`,
                        }}
                    ></span>
                        <span className="label-text">{item.label}</span>
                    </div>
                    <div className="column due-date">
                        {new Date(item.endTime).toLocaleDateString()}
                    </div>
                    {/*<div className="column members">*/}
                    {/*    {(item.members?.$values || []).map((member, index) => (*/}
                    {/*        <div key={index} className="member">*/}
                    {/*            <img*/}
                    {/*                src={member.avatar}*/}
                    {/*                alt={member.email}*/}
                    {/*                className="member-avatar"*/}
                    {/*            />*/}
                    {/*            <span className="member-email">{member.email}</span>*/}
                    {/*        </div>*/}
                    {/*    ))}*/}
                    {/*</div>*/}
                </div>
            </div>
            {isColorPickerOpen && (
                <div className="color-picker-modal">
                    <div className="color-picker-modal-content">
                        <button className="close-button" onClick={handleClosePicker}>
                            ✖ Close
                        </button>
                        <ChromePicker color={labelColor} onChange={handleColorChange}/>
                    </div>
                </div>
            )}
        </>
    );
}