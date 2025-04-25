import React from "react";
import { useDispatch } from "react-redux";
import { useRootState } from "../../redux/hooks.ts";
import { editAvatarAsync, getAllAvatarsAsync } from "../../redux/actions/authenticateAction.ts";
import { useEffect } from "react";
import { baseUrl } from "../../axios/baseUrl.ts";
import "../../styles/user/profile-style.scss";
import {useNavigate} from "react-router-dom";

export const ProfilePage = () => {
    const dispatch = useDispatch();
    const editAvatar = useRootState((s) => s.authenticate.editAvatar);
    const avatars = useRootState((s) => s.authenticate.avatars);
    const userId = localStorage.getItem("user_profile_id");
    const navigate = useNavigate();

    const [formData, setFormData] = React.useState({
        userId: userId || '',
        avatarId: '',
    });

    useEffect(() => {
        dispatch(getAllAvatarsAsync());
    }, [dispatch]);

    useEffect(() => {
        if (editAvatar) {
            setFormData({
                userId: userId || '',
                avatarId: editAvatar.avatarId,
            });
        }
    }, [editAvatar, userId]);

    const handleAvatarSelect = (avatarId: string) => {
        setFormData((prev) => ({ ...prev, avatarId }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!formData.userId) {
            console.warn("User ID is missing. Cannot edit avatar.");
            return;
        }
        await dispatch(editAvatarAsync(formData));
        window.location.reload();
    };

    console.log("USER ID", userId);

    return (
        <div className="profile-container">
            <form onSubmit={handleSubmit} className="profile-edit-form">
                <div className="avatar-selection">
                    <p>Choose your avatar:</p>
                    <div className="avatar-list">
                        {avatars?.map((avatar) => (
                            <img
                                key={avatar.id}
                                src={`${baseUrl}/images/avatars/${avatar.name}.png`}
                                alt={avatar.name}
                                className={`avatar-option ${formData.avatarId === avatar.id ? 'selected' : ''}`}
                                onClick={() => handleAvatarSelect(avatar.id)}
                            />
                        ))}
                    </div>
                </div>

                <button type="submit">Save Changes</button>
            </form>
        </div>
    );
};