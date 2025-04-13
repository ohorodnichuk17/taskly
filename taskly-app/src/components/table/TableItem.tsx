import "../../styles/table/table-item-styles.scss"

export default function TableItem({ item }: TableItemProps) {
    return (
        <div className="table-item">
            <div className="table-item-header">
                <h4>Task</h4>
                <h4>Status</h4>
                <h4>Label</h4>
                <h4>Due Date</h4>
            </div>

            <div className="table-item-content">
                <div className="column task">{item.task}</div>
                <div className="column status">
                    <span
                        className={`status ${
                            item.status === "To Do"
                                ? "status--todo"
                                : item.status === "In Progress"
                                    ? "status--in-progress"
                                    : "status--done"
                        }`}
                    >
                        {item.status}
                    </span>
                </div>
                <div className="column label">
                    <span
                        className={`label-dot ${
                            item.label === "Green"
                                ? "label--green"
                                : item.label === "Red"
                                    ? "label--red"
                                    : item.label === "Blue"
                                        ? "label--blue"
                                        : "label--yellow"
                        }`}
                    ></span>
                    <span className="label-text">{item.label}</span>
                </div>
                <div className="column due-date">
                    {new Date(item.endTime).toLocaleDateString()}
                </div>
            </div>
        </div>
    );
}
