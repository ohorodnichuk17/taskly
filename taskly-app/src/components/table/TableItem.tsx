import { useEffect, useState } from "react";
import {
    deleteTableItem,
    markTableItemAsCompleted,
    editTableItem,
    getTableItems
} from "../../redux/actions/tablesAction.ts";
import { useDispatch } from "react-redux";
import { ITableItem } from "../../interfaces/tableInterface.ts";
import {useParams} from "react-router-dom";
import "../../styles/table/main.scss";

interface TableItemProps {
    item: ITableItem;
}

export default function TableItem({ item }: TableItemProps) {
    const dispatch = useDispatch();
    const { tableId } = useParams();
    const normalizeStatus = (status: string): string =>
        status.trim().toLowerCase().replace(/\s+/g, "");

    const [editField, setEditField] = useState<null | keyof ITableItem>(null);
    const [editedItem, setEditedItem] = useState<ITableItem>({ ...item });
    const [isModalOpen, setIsModalOpen] = useState(false);

    const handleDeleteTableItem = async (itemId: string) => {
        try {
            await dispatch(deleteTableItem(itemId));
            window.location.reload();
        } catch (err) {
            console.error("Failed to delete table item:", err);
        }
    };

    const handleIsCompletedTableItem = async (tableItemId: string, isCompleted: boolean) => {
        try {
            const newStatus = isCompleted ? "Done" : editedItem.status;

            await dispatch(editTableItem({
                id: tableItemId,
                text: editedItem.task,
                status: newStatus,
                endTime: new Date(editedItem.endTime).toISOString(),
                label: editedItem.label,
            }));

            await dispatch(markTableItemAsCompleted({ tableItemId, isCompleted }));

            dispatch({
                type: "tableSlice/markTableItemAsCompleted",
                payload: { tableItemId, isCompleted },
            });
        } catch (err) {
            console.error("Failed to mark table item as completed:", err);
        }
    };

    const saveChanges = async () => {
        try {
            const payload = {
                Id: editedItem.id,
                Text: editedItem.task,
                Status: editedItem.status,
                EndTime: new Date(editedItem.endTime).toISOString(),
                Label: editedItem.label,
            };

            await dispatch(editTableItem(payload));

            if (!tableId) return;

            await dispatch(getTableItems(tableId));

        } catch (err) {
            console.error("Failed to save item edits:", err);
        } finally {
            setEditField(null);
        }
    };

    useEffect(() => {
        setEditedItem({ ...item });
    }, [item]);

    const getLabelColor = (label: string): string => {
        switch (label) {
            case "Info":
                return "#2196f3";
            case "Danger":
                return "#ff5252";
            case "Warning":
                return "#ff9800";
            case "Success":
                return "#4caf50";
            default:
                return "#ffffff";
        }
    };

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
                    {isModalOpen && (
                        <div className="custom-modal">
                            <div className="custom-modal-content">
                                <h3>Unable to Change Status</h3>
                                <p>
                                    You cannot change the status of an item marked as "Done" unless you first mark it as incomplete.
                                </p>
                                <div className="modal-actions">
                                    <button
                                        className="confirm-button"
                                        onClick={() => {
                                            setIsModalOpen(false);
                                        }}
                                    >
                                        OK
                                    </button>
                                </div>
                            </div>
                        </div>
                    )}
                    <div className={`column task ${item.isCompleted ? "task--completed" : ""}`}>
                        {editField === "task" ? (
                            <input
                                className="edit-field edit-field-active"
                                value={editedItem.task}
                                onChange={(e) => setEditedItem({...editedItem, task: e.target.value})}
                                onBlur={saveChanges}
                                autoFocus
                            />
                        ) : (
                            <div onClick={() => setEditField("task")}>{item.task}</div>
                        )}
                    </div>

                    <div className="column status">
                        {editField === "status" ? (
                            editedItem.isCompleted ? (
                                <>
                                    <span className="warning-text">Cannot change status from 'Done' unless the item is uncompleted.</span>
                                    <select
                                        disabled
                                        value={editedItem.status}
                                        className="edit-field"
                                    >
                                        <option value="ToDo">To Do</option>
                                        <option value="InProgress">In Progress</option>
                                        <option value="Done">Done</option>
                                    </select>
                                </>
                            ) : (
                                <select
                                    className="edit-field edit-field-active"
                                    value={editedItem.status}
                                    onChange={(e) => {
                                        console.log("📝 Status changed to:", e.target.value);
                                        setEditedItem({...editedItem, status: e.target.value});
                                    }}
                                    onBlur={saveChanges}
                                    autoFocus
                                >
                                    <option value="ToDo">To Do</option>
                                    <option value="InProgress">In Progress</option>
                                    <option value="Done">Done</option>
                                </select>
                            )
                        ) : (
                            <span
                                className={`status ${
                                    normalizeStatus(item.status) === "todo"
                                        ? "status--todo"
                                        : normalizeStatus(item.status) === "inprogress"
                                            ? "status--in-progress"
                                            : "status--done"
                                }`}
                                onClick={() => {
                                    if (item.isCompleted) {
                                        setIsModalOpen(true);
                                    } else {
                                        setEditField("status");
                                    }
                                }}
                            >
                              {item.status}
                            </span>
                        )}
                    </div>

                    <div className="column label">
                        {editField === "label" ? (
                            <select
                                className="edit-field edit-field-active"
                                value={editedItem.label}
                                onChange={(e) => setEditedItem({...editedItem, label: e.target.value})}
                                onBlur={saveChanges}
                                autoFocus
                            >
                                <option value="None">None</option>
                                <option value="Info">Info</option>
                                <option value="Danger">Danger</option>
                                <option value="Warning">Warning</option>
                                <option value="Success">Success</option>
                            </select>
                        ) : (
                            <div onClick={() => setEditField("label")}>
                                  <span
                                      className="label-dot"
                                      style={{
                                          backgroundColor: getLabelColor(item.label),
                                          boxShadow: `0 0 5px ${getLabelColor(item.label)}`,
                                      }}
                                  />
                                <span className="label-text">{item.label}</span>
                            </div>
                        )}
                    </div>

                    <div className="column due-date">
                        {editField === "endTime" ? (
                            <input
                                type="date"
                                className="edit-field edit-field-active"
                                value={new Date(editedItem.endTime).toISOString().slice(0, 10)}
                                onChange={(e) =>
                                    setEditedItem({...editedItem, endTime: new Date(e.target.value)})
                                }
                                onBlur={saveChanges}
                                autoFocus
                            />
                        ) : (
                            <div onClick={() => setEditField("endTime")}>
                                {new Date(item.endTime).toLocaleDateString()}
                            </div>
                        )}
                    </div>

                    <div className="column is-completed">
                        <div
                            className={`completion-circle ${item.isCompleted ? "completed" : ""}`}
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
        </>
    );
}
