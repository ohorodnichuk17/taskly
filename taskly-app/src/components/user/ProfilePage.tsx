import { useAppDispatch, useRootState } from "../../redux/hooks.ts";
import { editAvatarAsync, getAllAvatarsAsync, getSolanaUserReferralCodeAsync } from "../../redux/actions/authenticateAction.ts";
import { useEffect } from "react";
import { baseUrl } from "../../axios/baseUrl.ts";
import "../../styles/user/profile-style.scss";
import React from "react";
import copy_icon from "../../assets/icon/copy_icon.png";

export const ProfilePage = () => {
    const dispatch = useAppDispatch();
    const editAvatar = useRootState((s) => s.authenticate.editAvatar);
    const avatars = useRootState((s) => s.authenticate.avatars);
    const authMethod = useRootState((s) => s.authenticate.authMethod);
    const jwtUserProfile = useRootState((state) => state.authenticate.userProfile);
    const solanaUserProfile = useRootState((state) => state.authenticate.solanaUserProfile);
    const solanaUserReferralCode = useRootState((state) => state.authenticate.solanaUserReferralCode);

    const getUserId = () => {
        if (authMethod === "jwt" && jwtUserProfile?.id) {
            return jwtUserProfile.id;
        }
        if (authMethod === "solana" && solanaUserProfile?.id) {
            return solanaUserProfile.id;
        }
        return localStorage.getItem("user_profile_id") || '';
    };

    const getSolanaUserReferralCode = async () => {
        await dispatch(getSolanaUserReferralCodeAsync(getUserId()))
    }

    const [formData, setFormData] = React.useState({
        userId: getUserId(),
        avatarId: '',
    });

    useEffect(() => {
        dispatch(getAllAvatarsAsync());
        getSolanaUserReferralCode();
    }, [dispatch]);

    useEffect(() => {
        const currentUserId = getUserId();
        setFormData((prev) => ({
            ...prev,
            userId: currentUserId,
        }));
    }, [jwtUserProfile?.id, solanaUserProfile?.id, authMethod]);

    useEffect(() => {
        if (editAvatar) {
            setFormData({
                userId: getUserId(),
                avatarId: editAvatar.avatarId,
            });
        }
    }, [editAvatar, authMethod, jwtUserProfile?.id, solanaUserProfile?.id]);

    const handleAvatarSelect = (avatarId: string) => {
        setFormData((prev) => ({ ...prev, avatarId }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const currentUserId = getUserId();
        if (!currentUserId) {
            console.warn("User ID is missing. Cannot edit avatar.");
            return;
        }
        await dispatch(editAvatarAsync({ ...formData, userId: currentUserId }));
        window.location.reload();
    };

    console.log("USER ID", getUserId());

    return (
        <div className="profile-container">
            <div className="user-referral-container">
                <p>YOUR REFERRAL CODE</p>
                <div className="referral-code">
                    <p>{solanaUserReferralCode}</p>
                    <button>
                        <img src={copy_icon} alt="Copy icon" />

                        Copy
                    </button>
                </div>
            </div>
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