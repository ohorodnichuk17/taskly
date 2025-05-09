import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ArrowLeft, CheckCircle, Lock, Target } from "lucide-react";
import { useAppDispatch, useRootState } from "../../redux/hooks.ts";
import { getChallengeByIdAsync, bookChallengeAsCompletedAsync, markChallengeAsCompletedAsync } from "../../redux/actions/gamificationAction.ts";
import solana_icon from "../../assets/icon/solana_icon.png";
import { Link } from "react-router-dom";
import { toast } from 'react-toastify';
import "../../styles/challenge/challenge-styles.scss"

export default function ChallengePage() {
    const { challengeId } = useParams<{ challengeId: string }>();
    const dispatch = useAppDispatch();
    const challenge = useRootState((state) => state.gamification.challenges?.find(c => c.id === challengeId));
    const solanaUserId = useRootState((state) => state.authenticate.solanaUserProfile?.id);
    const [, setIsBookedByUser] = useState(challenge?.isBooked || false);
    const [, setIsCompletedByUser] = useState(challenge?.isCompleted || false);
    const [showModal, setShowModal] = useState(false);
    const [showErrorModal, setShowErrorModal] = useState(false);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    useEffect(() => {
        if (challengeId) {
            dispatch(getChallengeByIdAsync(challengeId));
        }
    }, [dispatch, challengeId]);

    useEffect(() => {
        setIsBookedByUser(challenge?.isBooked || false);
        setIsCompletedByUser(challenge?.isCompleted || false);
    }, [challenge?.isBooked, challenge?.isCompleted]);

    const formatDate = (dateString: string | undefined): string => {
        if (!dateString) return 'Unknown';
        const date = new Date(dateString);
        const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
        return date.toLocaleDateString('en-US', options);
    };

    const handleBookChallenge = async () => {
        if (!challengeId || !solanaUserId) return;
        const result = await dispatch(bookChallengeAsCompletedAsync({ challengeId, userId: solanaUserId }));
        if (bookChallengeAsCompletedAsync.fulfilled.match(result)) {
            setIsBookedByUser(true);
            setShowModal(true);
            dispatch(getChallengeByIdAsync(challengeId));
        } else {
            toast.error("Failed to book challenge.");
        }
    };

    const handleMarkAsCompleted = async () => {
        if (!challengeId) return;
        try {
            const result = await dispatch(markChallengeAsCompletedAsync(challengeId));
            if (markChallengeAsCompletedAsync.fulfilled.match(result)) {
                setIsCompletedByUser(true);
                toast.success("Challenge marked as completed!");
                dispatch(getChallengeByIdAsync(challengeId));
            } else if (markChallengeAsCompletedAsync.rejected.match(result)) {
                setErrorMessage("You have not yet completed the challenge.");
                setShowErrorModal(true);
            } else {
                setErrorMessage("An unexpected error occurred.");
                setShowErrorModal(true);
            }
        } catch (error: any) {
            setErrorMessage(error?.message || "Failed to communicate with the server.");
            setShowErrorModal(true);
        }
    };


    if (!challenge) {
        return (
            <div className="challenge-page loading">
                <Link to="/challenges" className="back-button">
                    <ArrowLeft size={20} /> Back to Challenges
                </Link>
                <h1 className="page-title">Loading Challenge...</h1>
            </div>
        );
    }

    return (
        <div className="challenge-page">
            {showModal && (
                <div className="modal-overlay">
                    <div className="modal">
                        <h2>Challenge Booked!</h2>
                        <p>Good luck! Once you're ready, mark it as completed.</p>
                        <button onClick={() => setShowModal(false)} className="close-button">
                            Got it!
                        </button>
                    </div>
                </div>
            )}
            {showErrorModal && errorMessage && (
                <div className="modal-overlay">
                    <div className="modal">
                        <h2>Error</h2>
                        <p>{errorMessage}</p>
                        <button onClick={() => setShowErrorModal(false)} className="close-button">
                            OK
                        </button>
                    </div>
                </div>
            )}


            <Link to="/challenges" className="back-button">
                <ArrowLeft size={20}/> Back to Challenges
            </Link>
            <h1 className="gradient-text">{challenge.name}</h1>
            <p className="challenge-description">{challenge.description}</p>

            <div className="challenge-details">
                <div className="detail-item">
                    <span className="detail-label">Starts:</span>
                    <span className="detail-value">{formatDate(challenge.startTime)}</span>
                </div>
                <div className="detail-item">
                    <span className="detail-label">Ends:</span>
                    <span className="detail-value">{formatDate(challenge.endTime)}</span>
                </div>
                <div className="detail-item">
                    <span className="detail-label">Target amount to complete challenge:</span>
                    <span className="detail-value">
                        <Target size={16} className="detail-icon"/> {challenge.targetAmount}
                    </span>
                </div>
                <div className="detail-item reward">
                    <span className="detail-label">Reward:</span>
                    <span className="detail-value">
                        <img src={solana_icon} alt="Solana" className="coin-icon"/> +{challenge.points} SOL
                    </span>
                </div>
                {challenge.isBooked && (
                    <div className="detail-item status">
                        <span className="detail-label">Status:</span>
                        <span className="detail-value">
                            {challenge.isCompleted ? <><CheckCircle size={16}
                                                                    className="status-icon completed"/> Completed</> : "Booked"}
                        </span>
                    </div>
                )}
                {!challenge.isActive && (
                    <div className="detail-item inactive">
                        <span className="detail-label">Status:</span>
                        <span className="detail-value">
                            <Lock size={16} className="status-icon locked"/> Inactive
                        </span>
                    </div>
                )}
            </div>

            <div className="challenge-actions">
                {!challenge?.isBooked && !challenge?.isCompleted && challenge.isActive && (
                    <button onClick={handleBookChallenge} className="book-button">
                        Book Challenge
                    </button>
                )}
                {challenge?.isBooked && challenge.isActive && !challenge.isCompleted && (
                    <button onClick={handleMarkAsCompleted} className="complete-button">
                        Mark as Completed
                    </button>
                )}
                {challenge?.isCompleted && (
                    <div className="challenge-completed-container">
                        <div className="completed-text">
                            Challenge Completed!
                        </div>
                    </div>
                )}
                {!challenge?.isActive && challenge?.isCompleted !== true && (
                    <div className="inactive-message">
                        <Lock size={24} className="locked-icon"/> Challenge Inactive
                    </div>
                )}
                {challenge?.isBooked && challenge?.isCompleted !== true && !challenge?.isActive && (
                    <div className="booked-inactive-message">
                        <Lock size={24} className="locked-icon"/> Already Booked (Inactive)
                    </div>
                )}
            </div>
        </div>
    );
}