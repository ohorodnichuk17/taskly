import {useEffect, useState} from "react";
import {useAppDispatch, useRootState} from "../../redux/hooks.ts";
import {setUserNameForSolanaUserAsync} from "../../redux/actions/authenticateAction.ts";
import "../../styles/authentication/set-username-for-solana-user.scss";
import {useNavigate} from "react-router-dom";

export default function SetUserNameForSolanaUser() {
    const [userName, setUserName] = useState('');
    const [publicKey, setPublicKey] = useState<string | null>(null);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const { userName: currentUserName } = useRootState((state) => state.authenticate);

    useEffect(() => {
        if (currentUserName) {
            navigate("/");
        }
    }, [currentUserName, navigate]);

    useEffect(() => {
        const key = localStorage.getItem('user_profile_publicKey');
        if (key) {
            setPublicKey(key);
        }
    }, []);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!publicKey || !userName) return;
        try {
            await dispatch(setUserNameForSolanaUserAsync({ publicKey, userName })).unwrap();
            navigate("/");
        } catch (error) {
            console.error("Failed to set username:", error);
        }
    };
    return (
        <div className="set-username-page">
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="userName" className="gradient-text">Username</label>
                    <input
                        id="userName"
                        type="text"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                        className="input-username"
                        required
                    />
                </div>
                <button
                    type="submit"
                    className="set-username-btn"
                    disabled={!publicKey}
                >
                    Set Username
                </button>
            </form>

        </div>
    );
}
