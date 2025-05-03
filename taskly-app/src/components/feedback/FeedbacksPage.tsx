import {useRootState} from "../../redux/hooks.ts";
import {useState, useEffect} from "react";
import {useDispatch} from "react-redux";
import {deleteFeedbackAsync, getAllFeedbacksAsync} from "../../redux/actions/feedbackAction.ts";
import {useNavigate} from "react-router-dom";
import "../../styles/feedback/feedbacks-page-styles.scss"
import {baseUrl} from "../../axios/baseUrl.ts";
import {getAllAvatarsAsync} from "../../redux/actions/authenticateAction.ts";
export default function FeedbacksPage() {
    const feedbacks = useRootState((state) => state.feedback.listOfFeedbacks);
    const jwtUserId = useRootState((state) => state.authenticate.userProfile?.id);
    const solanaUserId = useRootState((state) => state.authenticate.solanaUserProfile?.id);
    const avatars = useRootState((s) => s.authenticate.avatars);
    const authMethod = useRootState(s => s.authenticate.authMethod);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const fetchFeedbacks = async () => {
        setIsLoading(true);
        setError(null);
        try {
            await dispatch(getAllFeedbacksAsync());
        } catch (err) {
            setError("Failed to load feedbacks. Please try again.");
        } finally {
            setIsLoading(false);
        }
    }
    useEffect(() => {
        fetchFeedbacks();
    }, [dispatch]);

    const handleCreateFeedbackClick = () => {
        navigate('/feedbacks/create');
    }
    const handleDeleteFeedbackClick = async (feedbackId: string) => {
        try {
            setIsLoading(true);
            await dispatch(deleteFeedbackAsync(feedbackId));
            fetchFeedbacks();
        } catch (err) {
            setError("Failed to delete feedback. Please try again.");
        } finally {
            setIsLoading(false);
        }
    }
    const renderStars = (rating: number) => {
        const stars = [];
        for (let i = 1; i <= 5; i++) {
            stars.push(
                <span
                    key={i}
                    className={`star ${i <= rating ? "filled" : ""}`}
                >
                ★
            </span>
            );
        }
        return <div className="star-rating-display">{stars}</div>;
    };

    useEffect(() => {
        fetchFeedbacks();
        dispatch(getAllAvatarsAsync());
    }, [dispatch]);

    const formatUsername = (username?: string | null): string => {
        if (!username) return "Unknown";
        const atIndex = username.indexOf('@');
        return atIndex !== -1 ? username.slice(0, atIndex) : username;
    };

    return (
        <div className="feedbacks-page">
            <header className="feedbacks-header">
                <h1 className="gradient-text">Feedbacks</h1>
                <button onClick={handleCreateFeedbackClick}>Create Feedback</button>
            </header>
            <div className="feedbacks-list">
                {isLoading ? (
                    <p>Loading feedbacks...</p>
                ) : error ? (
                    <p className="error-message">{error}</p>
                ) : feedbacks && feedbacks.length > 0 ? (
                    feedbacks.map((feedback) => (
                        <div key={feedback.id} className="feedback-item">
                            {feedback.user && (
                                <div className="feedback-user">
                                    {(() => {
                                        const avatar = avatars?.find((a) => a.id === feedback.user.avatarId);
                                        return (
                                            <>
                                                <img
                                                    src={`${baseUrl}/images/avatars/${avatar?.name}.png`}
                                                    alt="avatar"
                                                    className="feedback-avatar"
                                                />
                                                <span className="user-name">
                                                    {authMethod === "solana" ? feedback.user.userName : formatUsername(feedback.user.email)}
                                              </span>
                                            </>
                                        );
                                    })()}
                                </div>
                            )}

                            <p>{feedback.review}</p>
                            {renderStars(feedback.rating)}

                            {(authMethod === "jwt" && feedback.userId === jwtUserId) ||
                            (authMethod === "solana" && feedback.userId === solanaUserId) ? (
                                <button onClick={() => handleDeleteFeedbackClick(feedback.id)}>Delete</button>
                            ) : null}
                        </div>
                    ))
                ) : (
                    <p>No feedbacks available. Create one!</p>
                )}
            </div>
        </div>
    );
}