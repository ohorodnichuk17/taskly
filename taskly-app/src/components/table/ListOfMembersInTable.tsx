import { useAppDispatch, useRootState } from "../../redux/hooks.ts";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {getAllMembersInTable, removeUserFromTable} from "../../redux/actions/tablesAction.ts";
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

    const jwtCurrentUser = useRootState((state) => state.authenticate.userProfile);
    const solanaCurrentUser = useRootState((state) => state.authenticate.solanaUserProfile);
    const authMethod = useRootState((state) => state.authenticate.authMethod);

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

    const handleRemoveMemberFromTable = async (memberEmail: string) => {
        try {
            setLoading(true);
            await dispatch(removeUserFromTable({ tableId, memberEmail }));
            fetchMembers();
        } catch (err) {
            setError("Failed to remove member from table. Please try again.");
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        console.log("Members:", members);
    }, [members]);

    useEffect(() => {
        fetchMembers();
    }, [tableId]);

    const handleAddMember = () => {
        navigate(`/tables/${tableId}/add-member`);
    };

    const filteredMembers = members?.filter((member) => {
        if (authMethod === "jwt" && jwtCurrentUser?.email && member.email) {
            return member.email !== jwtCurrentUser.email;
        } else if (authMethod === "solana" && solanaCurrentUser?.userName && member.userName) {
            return member.userName !== solanaCurrentUser.userName;
        }
        return true;
    });

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
            ) : filteredMembers && filteredMembers.length > 0 ? (
                <ul className="members-list">
                    {filteredMembers.map((member) => {
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
                                <button className="delete-btn"
                                        onClick={() => handleRemoveMemberFromTable(member.email)}>
                                    <svg className="trash-icon" xmlns="http://www.w3.org/2000/svg" height="20"
                                         viewBox="0 0 24 24" width="20" fill="#ffffff">
                                        <path d="M0 0h24v24H0z" fill="none"/>
                                        <path d="M16 9v10H8V9h8m-1.5-6h-5l-1 1H5v2h14V4h-4.5l-1-1z"/>
                                    </svg>
                                </button>
                            </li>
                        );
                    })}
                </ul>
            ) : (
                <div className="no-members">
                    <p>No members found.</p>
                    <button className="add-member-btn" onClick={handleAddMember}>
                        Add Member
                    </button>
                </div>
            )}
        </div>
    );
}
