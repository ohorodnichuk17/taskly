import { useNavigate, useParams } from "react-router-dom";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { useRootState } from "../../redux/hooks.ts";
import { createTable, editTable } from "../../redux/actions/tablesAction.ts";
import "../../styles/table/main.scss";

export default function TableFormPage() {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const { tableId } = useParams();
    const jwtUserId = useRootState((state) => state.authenticate.userProfile?.id);
    const solanaUserId = useRootState((state) => state.authenticate.solanaUserProfile?.id);
    const authMethod = useRootState((state) => state.authenticate.authMethod);
    const [name, setName] = useState<string>("");
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const isEditMode = !!tableId;

    const handleSubmit = async () => {
        if (!name.trim()) return;

        setIsLoading(true);
        setError(null);

        try {
            if (isEditMode && tableId) {
                await dispatch(editTable({ tableId, tableName: name }));
            } else {
                let currentUserId: string | undefined;
                if (authMethod === "jwt") {
                    currentUserId = jwtUserId;
                } else if (authMethod === "solana") {
                    currentUserId = solanaUserId;
                }

                if (currentUserId) {
                    await dispatch(createTable({ name, userId: currentUserId }));
                } else {
                    console.warn("User ID is missing for the current authentication method.");
                    setError("Could not create table: User ID not found.");
                }
            }
            navigate("/tables");
        } catch {
            setError(`Failed to ${isEditMode ? "edit" : "create"} table. Please try again.`);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="create-table-page">
            <header className="create-table-header">
                <h1 className="gradient-text">{isEditMode ? "Edit Table" : "Create a New Table"}</h1>
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
                    onClick={handleSubmit}
                    disabled={isLoading || !name.trim()}
                >
                    {isLoading ? (isEditMode ? "Editing..." : "Creating...") : isEditMode ? "Edit Table" : "Create Table"}
                </button>
                {error && <p className="error-message">{error}</p>}
            </div>
        </div>
    );
}
