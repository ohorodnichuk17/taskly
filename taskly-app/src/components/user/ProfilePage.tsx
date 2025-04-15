import React from 'react';
import { getAllAvatarsAsync, editUserProfileAsync } from '../../redux/actions/authenticateAction';
import { useRootState } from '../../redux/hooks';
import '../../styles/user/profile-style.scss';
import {useDispatch} from "react-redux";
import {useEffect} from "react";
import {baseUrl} from "../../axios/baseUrl.ts";

export const ProfilePage = () => {
    const dispatch = useDispatch();
    const userProfile = useRootState((s) => s.authenticate.userProfile);
    const avatars = useRootState((s) => s.authenticate.avatars);
    const [formData, setFormData] = React.useState({
        username: userProfile?.username || '',
        email: userProfile?.email || '',
        avatarId: userProfile?.avatarId || '',
    });

    useEffect(() => {
        dispatch(getAllAvatarsAsync());
    }, [dispatch]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await dispatch(editUserProfileAsync(formData));
    };

    return (
        <div className="profile-container">
            <div className="main-information">
                <div className="avatar">
                    <img
                        src={`${baseUrl}/images/avatars/${formData.avatarId}.png`}
                        alt="User Avatar"
                    />
                </div>
                <div className="name-and-buttons">
                    <p>{formData.username}</p>
                </div>
            </div>
            <form onSubmit={handleSubmit} className="profile-edit-form">
                <label>
                    Username:
                    <input
                        type="text"
                        name="username"
                        value={formData.username}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    Email:
                    <input
                        type="email"
                        name="email"
                        value={formData.email}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    Avatar:
                    <select name="avatarId" value={formData.avatarId} onChange={handleChange}>
                        {avatars?.map((avatar) => (
                            <option key={avatar.id} value={avatar.id}>
                                {avatar.name}
                            </option>
                        ))}
                    </select>
                </label>
                <button type="submit">Save Changes</button>
            </form>
        </div>
    );
};
