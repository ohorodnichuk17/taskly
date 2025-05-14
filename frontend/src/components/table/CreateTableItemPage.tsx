import { useNavigate, useParams } from "react-router-dom";
import { useState } from "react";
import { createTableItem } from "../../redux/actions/tablesAction.ts";
import "../../styles/table/main.scss";
import {useAppDispatch, useRootState} from "../../redux/hooks.ts";

export function CreateTableItemPage() {
    const { tableId } = useParams();
    const [text, setText] = useState("");
    const [status, setStatus] = useState("To Do");
    const [label, setLabel] = useState("");
    const [endTime, setEndTime] = useState<string>("");
    const [isCompleted,] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const userId = useRootState((state) => state.authenticate.userProfile?.id || state.authenticate.solanaUserProfile?.id);

    const handleSubmit = async () => {
        if (!text.trim() || !status.trim() || !label.trim() || !endTime.trim()) {
            setError("Please fill in all fields.");
            return;
        }

        if (!tableId) {
            return;
        }

        setIsLoading(true);
        setError(null);

        try {
            const formattedEndTime = new Date(endTime);
            await dispatch(createTableItem({ task: text, status, label, members: userId ? [userId] : [], endTime: formattedEndTime, isCompleted, tableId }));
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
                    value={text}
                    onChange={(e) => setText(e.target.value)}
                    className="input-task"
                />
                <select
                    value={status}
                    onChange={(e) => setStatus(e.target.value)}
                    className="select-status"
                >
                    <option value="ToDo">To Do</option>
                    <option value="InProgress">In Progress</option>
                    <option value="Done">Done</option>
                </select>
                <select
                    value={label}
                    onChange={(e) => setLabel(e.target.value)}
                    className="select-label"
                >
                    <option value="">None</option>
                    <option value="Info" style={{ color: '#2196F3' }}>Info</option>
                    <option value="Warning" style={{ color: '#FFC107' }}>Warning</option>
                    <option value="Danger" style={{ color: '#F44336' }}>Danger</option>
                    <option value="Success" style={{ color: '#4CAF50' }}>Success</option>
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
                    disabled={isLoading || !text.trim() || !status.trim() || !label.trim() || !endTime.trim()}
                >
                    {isLoading ? "Creating..." : "Create"}
                </button>
                {error && <p className="error-message">{error}</p>}
            </div>
        </div>
    );
}