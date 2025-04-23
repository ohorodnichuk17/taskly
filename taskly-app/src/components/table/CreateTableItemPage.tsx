import { useNavigate, useParams } from "react-router-dom";
import { useState } from "react";
import { IUserForTable } from "../../interfaces/tableInterface.ts";
import { createTableItem } from "../../redux/actions/tablesAction.ts";
import { useDispatch } from "react-redux";
import "../../styles/table/create-table-item-page-style.scss";

export function CreateTableItemPage() {
    const { tableId } = useParams();
    const [task, setTask] = useState("");
    const [status, setStatus] = useState("To Do");
    const [label, setLabel] = useState("");
    const [members, setMembers] = useState<IUserForTable[]>([]);
    const [endTime, setEndTime] = useState<string>("");
    const [isCompleted, setIsCompleted] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const handleSubmit = async () => {
        if (!task.trim() || !status.trim() || !label.trim() || !endTime.trim()) {
            setError("Please fill in all fields.");
            return;
        }

        if (!tableId) {
            return;
        }

        setIsLoading(true);
        setError(null);

        try {
            await dispatch(createTableItem({ task, status, label, members, endTime, isCompleted, tableId }));
            navigate(`/tables/${tableId}`);
        } catch (err) {
            setError("Failed to create table item. Please try again.");
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="create-table-item-page">
            <header className="create-table-item-header">
                <h1 className="gradient-text">Create new Task</h1>
            </header>
            <div className="form-container">
                <input
                    type="text"
                    placeholder="Enter task"
                    value={task}
                    onChange={(e) => setTask(e.target.value)}
                    className="input-task"
                />
                <select
                    value={status}
                    onChange={(e) => setStatus(e.target.value)}
                    className="select-status"
                >
                    <option value="To Do">To Do</option>
                    <option value="In Progress">In Progress</option>
                    <option value="Done">Done</option>
                </select>
                <select
                    value={label}
                    onChange={(e) => setLabel(e.target.value)}
                    className="select-label"
                >
                    <option value="">None</option>
                    <option value="info" style={{ color: '#2196F3' }}>Info</option>
                    <option value="warning" style={{ color: '#FFC107' }}>Warning</option>
                    <option value="danger" style={{ color: '#F44336' }}>Danger</option>
                    <option value="success" style={{ color: '#4CAF50' }}>Success</option>
                </select>
                <input
                    type="date"
                    value={endTime}
                    onChange={(e) => setEndTime(e.target.value)}
                    className="input-end-time"
                />
                <button
                    className="create-table-item-btn"
                    onClick={handleSubmit}
                    disabled={isLoading || !task.trim() || !status.trim() || !label.trim() || !endTime.trim()}
                >
                    {isLoading ? "Creating..." : "Create"}
                </button>
                {error && <p className="error-message">{error}</p>}
            </div>
        </div>
    );
}