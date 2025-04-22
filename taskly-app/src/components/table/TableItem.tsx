import "../../styles/table/table-item-styles.scss";
import {useEffect, useState} from "react";
import { ChromePicker } from "react-color";
import {useNavigate, useParams} from "react-router-dom";
import {
    addTableItem,
    deleteTableItem,
    markTableItemAsCompleted
} from "../../redux/actions/tablesAction.ts";
import {useDispatch} from "react-redux";
import {ITableItem} from "../../interfaces/tableInterface.ts";

export default function TableItem({ item }: ITableItem) {
    const {tableId} = useParams();
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

    const handleDeleteTableItem = async (itemId: string) => {
        try {
            await dispatch(deleteTableItem(itemId));
            window.location.reload();
        } catch (err) {
            console.error("Failed to delete table item:", err);
        }
    }

    const handleIsCompletedTableItem = async (tableItemId: string, isCompleted: boolean) => {
        try {
            await dispatch(markTableItemAsCompleted({ tableItemId, isCompleted }));
            dispatch({
                type: "tableSlice/markTableItemAsCompleted",
                payload: { tableItemId, isCompleted },
            });
        } catch (err) {
            console.error("Failed to mark table item as completed:", err);
        }
    };

    useEffect(() => {
        console.log("Item updated:", item);
    }, [item]);

    return (
        <>
            <div className="table-item">
                <div className="table-item-header">
                    <h4>Task</h4>
                    <h4>Status</h4>
                    <h4>Label</h4>
                    <h4>Due Date</h4>
                    <h4>Is Completed</h4>
                    <h4>Actions</h4>
                </div>

                <div className="table-item-content">
                    <div className={`column task ${item.isCompleted ? "task--completed" : ""}`}>
                        {item.task}
                    </div>
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
                    <div className="column is-completed">
                        <div
                            className={`completion-circle ${item.isCompleted === true ? "completed" : ""}`}
                            onClick={() => handleIsCompletedTableItem(item.id, !item.isCompleted)}
                            title="Mark as completed"
                        ></div>
                    </div>
                    <div className="column actions">
                        <button className="delete-btn" onClick={() => handleDeleteTableItem(item.id)}>
                            <svg className="trash-icon" xmlns="http://www.w3.org/2000/svg" height="20"
                                 viewBox="0 0 24 24" width="20" fill="#ffffff">
                                <path d="M0 0h24v24H0z" fill="none"/>
                                <path d="M16 9v10H8V9h8m-1.5-6h-5l-1 1H5v2h14V4h-4.5l-1-1z"/>
                            </svg>
                        </button>
                    </div>
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