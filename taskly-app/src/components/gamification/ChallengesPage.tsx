import { useEffect, useState } from "react";
import { Lock, Target } from "lucide-react";
import { useAppDispatch, useRootState } from "../../redux/hooks.ts";
import { IChallengeResponse } from "../../interfaces/gamificationInterface.ts";
import {deleteChallengeAsync, getAllChallengesAsync} from "../../redux/actions/gamificationAction.ts";
import "../../styles/challenge/challenges-styles.scss";
import {Link, useNavigate} from "react-router-dom";
import solana_icon from "../../assets/icon/solana_icon.png";

const ITEMS_PER_PAGE_MOBILE = 1;
const ITEMS_PER_PAGE_DESKTOP = 6;

export default function ChallengesPage() {
    const dispatch = useAppDispatch();
    const challenges = useRootState((state) => state.gamification.challenges);
    const [currentPage, setCurrentPage] = useState(1);
    const [isMobile, setIsMobile] = useState(window.innerWidth <= 768);
    const currentUserId = useRootState((state) => state.authenticate.solanaUserProfile?.id);
    const navigate = useNavigate();
    const userProfile = useRootState((state) => state.authenticate.userProfile);

    useEffect(() => {
        dispatch(getAllChallengesAsync());
    }, [dispatch]);

    useEffect(() => {
        const handleResize = () => {
            setIsMobile(window.innerWidth <= 768);
            setCurrentPage(1);
        };

        window.addEventListener("resize", handleResize);
        return () => window.removeEventListener("resize", handleResize);
    }, []);

    const itemsPerPage = isMobile ? ITEMS_PER_PAGE_MOBILE : ITEMS_PER_PAGE_DESKTOP;
    const totalPages = Math.ceil((challenges?.length || 0) / itemsPerPage);
    const startIdx = (currentPage - 1) * itemsPerPage;
    const paginatedChallenges = challenges?.slice(startIdx, startIdx + itemsPerPage);

    const handlePrev = () => {
        if (currentPage > 1) setCurrentPage((prev) => prev - 1);
    };

    const handleNext = () => {
        if (currentPage < totalPages) setCurrentPage((prev) => prev + 1);
    };

    const formatDate = (dateString: string | undefined): string => {
        if (!dateString) return 'Unknown';
        const date = new Date(dateString);
        const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: 'long', day: 'numeric' };
        return date.toLocaleDateString('en-US', options);
    };

    const handleCreateChallengeClick = () => {
        navigate('/challenges/create');
    }

    const handleDeleteChallenge = async (challengeId: string) => {
        try {
            await dispatch(deleteChallengeAsync(challengeId));
            dispatch(getAllChallengesAsync());
        } catch (error) {
            console.error("Failed to delete challenge:", error);
        }
    }

    return (
        <div className="challenges-page">
            <header className="challenges-header">
                <h1 className="gradient-text">Crypto Challenges</h1>
                {userProfile?.email === 'tasklytodolist@gmail.com' && (
                    <button className="create-challenge-btn" onClick={handleCreateChallengeClick}>
                        <span className="icon">＋</span> Challenge
                    </button>
                )}
            </header>
            {Array.isArray(challenges) ? (
                challenges.length > 0 ? (
                    <>
                        <div className="challenges-list">
                            {paginatedChallenges?.map((challenge: IChallengeResponse) => {
                                const isBookedByOther = challenge.isBooked && challenge.userId !== currentUserId;
                                const isLocked = (isBookedByOther || !challenge.isActive) && !challenge.isCompleted;
                                const isEnded = new Date(challenge.endTime) < new Date();

                                return (
                                    <div
                                        key={challenge.id}
                                        className={`challenge-item animated ${challenge.isCompleted ? "completed" : isLocked ? "locked" : ""}`}
                                    >
                                        {challenge.isCompleted && (
                                            <div className="completed-overlay-text">
                                                <span>Already Conquered!</span>
                                            </div>
                                        )}
                                        <div className={`challenge-content ${challenge.isCompleted ? "blurred" : ""}`}>
                                            {(isEnded || isLocked) && (
                                                <div className="lock-overlay">
                                                    <Lock size={32} className="lock-icon"/>
                                                    <span>
                            {isEnded
                                ? "Challenge Ended"
                                : isBookedByOther
                                    ? "Already booked"
                                    : `Starts on ${formatDate(challenge.startTime)}`}
                        </span>
                                                </div>
                                            )}

                                            <h2 className="challenge-name">{challenge.name}</h2>
                                            <p className="challenge-dates">
                                                {formatDate(challenge.startTime)} <span
                                                className="date-separator">—</span> {formatDate(challenge.endTime)}
                                            </p>
                                            <div className="challenge-stats">
                                                <div className="stat-block target">
                                                    <Target size={16} className="stat-icon"/> {challenge.targetAmount}
                                                </div>
                                                <div className="stat-block points reward">
                                                    <img src={solana_icon} alt="Solana" className="coin-icon"/>
                                                    +{challenge.points}
                                                </div>
                                            </div>
                                            <Link to={`/challenges/${challenge.id}`}>
                                                <button className="details-button">View Details</button>
                                            </Link>
                                        </div>
                                        {userProfile?.email === 'tasklytodolist@gmail.com' && (
                                            <button className="delete-btn"
                                                    onClick={() => handleDeleteChallenge(challenge.id)}>
                                                <svg
                                                    className="trash-icon"
                                                    xmlns="http://www.w3.org/2000/svg"
                                                    height="20"
                                                    viewBox="0 0 24 24"
                                                    width="20"
                                                    fill="#ffffff"
                                                >
                                                    <path d="M0 0h24v24H0z" fill="none"/>
                                                    <path d="M16 9v10H8V9h8m-1.5-6h-5l-1 1H5v2h14V4h-4.5l-1-1z"/>
                                                </svg>
                                            </button>
                                        )}
                                    </div>
                                );
                            })}
                        </div>
                        {totalPages > 1 && (
                            <div className="pagination-controls">
                                <button onClick={handlePrev} disabled={currentPage === 1}>
                                    Previous
                                </button>
                                <button onClick={handleNext} disabled={currentPage === totalPages}>
                                    Next
                                </button>
                            </div>
                        )}
                    </>
                ) : (
                    <p>No challenges found.</p>
                )
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );
}