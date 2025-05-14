import {useNavigate, useParams} from "react-router-dom";
import {useState} from "react";
import {addUserToTable} from "../../redux/actions/tablesAction.ts";
import "../../styles/table/main.scss"
import {useAppDispatch} from "../../redux/hooks.ts";

export default function AddMemberToTablePage() {
    const {tableId} = useParams();
    const [memberEmail, setMemberEmail] = useState("");
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    const handleSubmit = async () => {
        if (!memberEmail.trim()) {
            setError("Please fill in all fields.");
            return;
        }
        if (!tableId) {
            return;
        }
        setIsLoading(true);
        setError(null);

        try {
            await dispatch(addUserToTable({tableId, memberEmail}));
            navigate(`/tables/${tableId}`);
        } catch (err) {
            setError("Failed to add member to table. Please try again.");
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="add-member-to-table-container">
            <header className="add-member-to-table-header">
                <h1 className="gradient-text">Add new member</h1>
            </header>
            <div className="form-container">
                <input
                    type="email"
                    placeholder="Enter member email"
                    value={memberEmail}
                    onChange={(e) => setMemberEmail(e.target.value)}
                    className="input-member-email"
                />
                <button
                    className="add-member-button"
                    onClick={handleSubmit}
                    disabled={isLoading || !memberEmail.trim()}
                >
                    {isLoading ? "Adding..." : "Add Member"}
                </button>
                {error && <p className="error-message">{error}</p>}
            </div>
        </div>
    );
}
