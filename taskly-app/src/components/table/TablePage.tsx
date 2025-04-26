import {useNavigate, useParams} from "react-router-dom";
import { useAppDispatch, useRootState } from "../../redux/hooks.ts";
import { getTableItems } from "../../redux/actions/tablesAction.ts";
import TableItem from "./TableItem";
import { useEffect, useState } from "react";
// import "../../styles/table/table-page-style.scss"
import "../../styles/table/main.scss";

export default function TablePage() {
    const { tableId } = useParams();
    const tableItems = useRootState((state) => state.table.tableItems);
    const dispatch = useAppDispatch();
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchTableItems = async () => {
            try {
                setIsLoading(true);
                await dispatch(getTableItems(tableId));
                setIsLoading(false);
            } catch (err) {
                setError("Failed to load table items. Please try again.");
                setIsLoading(false);
            }
        };

        if (tableId) {
            fetchTableItems();
        }
    }, [dispatch, tableId]);

    useEffect(() => {
        console.log("Table items:", tableItems);
    }, []);

    return (
        <div className="table-page">
            <div className="table-header">
                <button
                    className="create-table-btn"
                    onClick={() => navigate(`/tables/${tableId}/add-member`)}
                >
                    <span className="icon">＋</span> Add Member To Table
                </button>
                <button
                    className="create-table-btn"
                    onClick={() => navigate(`/tables/${tableId}/create`)}
                >
                    <span className="icon">＋</span> Add Task
                </button>
            </div>
            <h1 className="gradient-text">Table</h1>
            {isLoading ? (
                <p>Loading...</p>
            ) : error ? (
                <p>{error}</p>
            ) : Array.isArray(tableItems) && tableItems.length > 0 ? (
                <div className="table-items">
                    {tableItems.map((item, index) => (
                        <TableItem key={index} item={item}/>
                    ))}
                </div>
            ) : (
                <p>No items available in this table.</p>
            )}
        </div>
    );
}