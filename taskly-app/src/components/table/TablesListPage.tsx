import "../../styles/table/tables-page-style.scss"
import { useRootState } from "../../redux/hooks.ts";
import { useDispatch } from "react-redux";
import { useEffect, useRef, useState } from "react";
import { deleteTable, getTablesByUser } from "../../redux/actions/tablesAction.ts";
import { Link, useNavigate } from "react-router-dom";

export default function TablesListPage() {
    const tables = useRootState((state) => state.table.listOfTables);
    const dispatch = useDispatch();
    const workspaceContainerRef = useRef<HTMLDivElement | null>(null);
    const [workSpaceOverflowY, setWorkspaceOverflowY] = useState<"auto" | "scroll">("auto");
    const userId = useRootState(s => s.authenticate.userProfile?.id);
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const fetchTables = async () => {
        if (userId) {
            await dispatch(getTablesByUser(userId));
        }
    };

    const handleDeleteTable = async (tableId: string) => {
        try {
            setIsLoading(true);
            await dispatch(deleteTable(tableId));
            fetchTables();
        } catch (err) {
            setError("Failed to delete table. Please try again.");
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        fetchTables();
        console.log("Tables from Redux:", tables);
    }, []);

    const handleCreateTableClick = () => {
        navigate('/tables/create');
    };

    useEffect(() => {
        if (workspaceContainerRef.current) {
            setWorkspaceOverflowY(
                workspaceContainerRef.current.offsetHeight < workspaceContainerRef.current.scrollHeight
                    ? "scroll"
                    : "auto"
            );
        }
    }, [workspaceContainerRef.current]);

    return (
        <div className="tables-page" ref={workspaceContainerRef} style={{ overflowY: workSpaceOverflowY }}>
            <header className="tables-header">
                <h1>
                    <span className="gradient-text">📋 My Tables</span>
                </h1>
                <button className="create-table-btn" onClick={handleCreateTableClick}>
                    <span className="icon">＋</span> Create
                </button>
            </header>
            <div className="tables-list">
                {tables && tables.length > 0 ? (
                    tables.map((table, index) => (
                        <div key={index} className="table-item">
                            <Link to={`${table.id}`} key={table.id} className="table-name-link">
                                <span>{table.name}</span>
                            </Link>
                            <button className="delete-btn" onClick={() => handleDeleteTable(table.id)}>
                                <svg className="trash-icon" xmlns="http://www.w3.org/2000/svg" height="20"
                                     viewBox="0 0 24 24" width="20" fill="#ffffff">
                                    <path d="M0 0h24v24H0z" fill="none"/>
                                    <path d="M16 9v10H8V9h8m-1.5-6h-5l-1 1H5v2h14V4h-4.5l-1-1z"/>
                                </svg>
                            </button>
                        </div>
                    ))
                ) : (
                    <p>No tables available. Create one!</p>
                )}
            </div>
        </div>
    );
}