import { useAppDispatch, useRootState } from "../../redux/hooks.ts";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getAllMembersInTable } from "../../redux/actions/tablesAction.ts";
import { baseUrl } from "../../axios/baseUrl.ts";
import { getAllAvatarsAsync } from "../../redux/actions/authenticateAction.ts";
import "../../styles/table/main.scss";

export default function ListOfMembersInTable() {
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const dispatch = useAppDispatch();
    const { tableId } = useParams();
    const navigate = useNavigate();

    const members = useRootState((state) => state.table.membersList);
    const avatars = useRootState((s) => s.authenticate.avatars);

    const currentUser = useRootState((state) => state.authenticate.userProfile);

    const fetchMembers = async () => {
        try {
            setLoading(true);
            await dispatch(getAllMembersInTable(tableId));
            await dispatch(getAllAvatarsAsync());
        } catch (err) {
            setError("Failed to load members. Please try again.");
            setLoading(false);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchMembers();
    }, [tableId]);

    return (
        <div className="members-page">
            <header className="members-header">
                <h1 className="gradient-text">Members</h1>
                <button
                    className="back-btn"
                    onClick={() => navigate(`/tables/${tableId}`)}
                >
                    Back to table
                </button>
            </header>
            {loading ? (
                <div>Loading...</div>
            ) : error ? (
                <div>{error}</div>
            ) : members && members.length > 0 ? (
                <ul className="members-list">
                    {members
                        .filter((member) => member.email !== currentUser?.email)
                        .map((member) => {
                            const avatar = avatars?.find((avatar) => avatar.id === member.avatarId);
                            return (
                                <li key={member.$id} className="member-item">
                                    <img
                                        src={
                                            avatar
                                                ? `${baseUrl}/images/avatars/${avatar.name}.png`
                                                : "/path/to/default-avatar.png"
                                        }
                                        alt={`${member.email}'s avatar`}
                                        className="member-avatar"
                                    />
                                    <span>{member.email}</span>
                                </li>
                            );
                        })}
                </ul>
            ) : (
                <div>No members found.</div>
            )}
        </div>
    );
}