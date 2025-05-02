import {useRootState} from "../../redux/hooks.ts";
import {useState} from "react";
import {useNavigate} from "react-router-dom";
import {useDispatch} from "react-redux";
import {createFeedbackAsync} from "../../redux/actions/feedbackAction.ts";
import "../../styles/feedback/create-feedback-styles.scss";

export default function CreateFeedbackPage() {
    const jwtUserId = useRootState((state) => state.authenticate.userProfile?.id);
    const solanaUserId = useRootState((state) => state.authenticate.solanaUserProfile?.id);
    const authMethod = useRootState((state) => state.authenticate.authMethod);

    const navigate = useNavigate();
    const dispatch = useDispatch();

    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const [review, setReview] = useState<string>("");
    const [rating, setRating] = useState<number>(0);

    const handleSubmit = async () => {
        if(!review.trim() || !rating) return;

        setIsLoading(true);
        setError(null);

        try {
            let currentUserId: string | undefined;
            if(authMethod == "jwt") {
                currentUserId = jwtUserId;
            } else if(authMethod == "solana") {
                currentUserId = solanaUserId;
            }

            if(currentUserId) {
                await dispatch(createFeedbackAsync({userId: currentUserId, review, rating, createdAt: new Date()}));
                navigate("/feedbacks");
            } else {
                console.warn("User ID is missing for the current authentication method.");
                setError("Could not create feedback: User ID not found.");
            }
        } catch (err) {
            setError("Failed to create feedback. Please try again.");
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="create-feedback-page">
            <header className="create-feedback-header">
                <h1 className="gradient-text">Create Feedback</h1>
            </header>
            <div className="form-container">
                <div className="star-rating">
                    {[1, 2, 3, 4, 5].map((star) => (
                        <span
                            key={star}
                            className={`star ${rating >= star ? "filled" : ""}`}
                            onClick={() => setRating(star)}
                        >
            ★
        </span>
                    ))}
                </div>
                <textarea
                    placeholder="Enter review"
                    value={review}
                    onChange={(e) => setReview(e.target.value)}
                    className="input-review"
                />
                <button onClick={handleSubmit} className="submit-button">
                    {isLoading ? "Creating..." : "Create Feedback"}
                </button>
                {error && <p className="error-message">{error}</p>}
            </div>
        </div>
    );
}