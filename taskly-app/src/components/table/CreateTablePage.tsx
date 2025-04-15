import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { useRootState } from "../../redux/hooks.ts";
import { useDispatch } from "react-redux";
import "../../styles/table/create-table-page-style.scss";
import {createTable} from "../../redux/actions/tablesAction.ts";

export default function CreateTablePage() {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const userId = useRootState((state) => state.authenticate.userProfile?.id);
    const [name, setName] = useState<string>("");
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const createNewTable = async () => {
        if (name && userId) {
            try {
                setIsLoading(true);
                await dispatch(createTable({name, userId}));
                navigate("/tables");
            } catch (err) {
                setError("Failed to create table. Please try again.");
            } finally {
                setIsLoading(false);
            }
        }
    };

    return (
        <div className="create-table-page">
            <header className="create-table-header">
                <h1>Create a New Table</h1>
            </header>
            <div className="create-table-content">
                <input
                    type="text"
                    placeholder="Enter table name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    className="input-table-name"
                />
                <button
                    className="create-table-btn"
                    onClick={createNewTable}
                    disabled={isLoading || !name.trim()}
                >
                    {isLoading ? "Creating..." : "Create Table"}
                </button>
                {error && <p className="error-message">{error}</p>}
            </div>
        </div>
    );
}
