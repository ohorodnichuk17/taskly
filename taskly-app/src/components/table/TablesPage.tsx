import "../../styles/table/tables-page-style.scss"

import { useRootState } from "../../redux/hooks.ts";
import { useDispatch } from "react-redux";
import { useEffect, useRef, useState } from "react";
import {getTablesByUser} from "../../redux/actions/tablesAction.ts";

export default function TablesPage() {
    const tables = useRootState((state) => state.table.listOfTables);
    const dispatch = useDispatch();
    const workspaceContainerRef = useRef<HTMLDivElement | null>(null);
    const [workSpaceOverflowY, setWorkspaceOverflowY] = useState<"auto" | "scroll">("auto");
    const userId = useRootState(s => s.authenticate.userProfile?.id);

    const fetchTables = async () => {
        if (userId) {
            await dispatch(getTablesByUser(userId));
        }
    };

    useEffect(() => {
        fetchTables();
        console.log("Tables from Redux:", tables);

    }, []);

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
        <div className="tables-page" ref={workspaceContainerRef} style={{overflowY: workSpaceOverflowY}}>
            <header className="tables-header">
                <h1>
                    <span className="gradient-text">📋 My Tables</span>
                </h1>
                <button className="create-table-btn" onClick={() => alert("Create New Table")}>
                    <span className="icon">＋</span> Create
                </button>
            </header>
            <div className="tables-list">
                {tables && tables.length > 0 ? (
                    tables.map((table, index) => (
                        <div key={index} className="table-item">
                            <span>{table.name}</span>
                        </div>
                    ))
                ) : (
                    <p>No tables available. Create one!</p>
                )}
            </div>
        </div>
    );
}