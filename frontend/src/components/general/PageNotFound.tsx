import { useNavigate } from 'react-router-dom';
import '../../styles/general/not-found-style.scss'

export const PageNotFound = () => {
    const navigate = useNavigate();
    return (
        <div className="not-found-container">
            <h1 className="not-found-title">404</h1>
            <p className="not-found-subtitle">
                Page Not Found
            </p>
            <button
                className="not-found-button"
                onClick={() => navigate("/")}
            >
                Go Home
            </button>
        </div>
    );
};