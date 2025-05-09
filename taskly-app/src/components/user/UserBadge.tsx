import React, { useEffect, useState } from "react";
import { useAppDispatch, useRootState } from "../../redux/hooks.ts";
import { getAllBadgesByUserIdAsync, getAllBadgesAsync } from "../../redux/actions/gamificationAction.ts";
import { baseUrl } from "../../axios/baseUrl.ts";
import "../../styles/user/user-badge-style.scss";
import { IBadgeResponse } from "../../interfaces/gamificationInterface.ts";

interface UserBadgeProps {
    userId: string | undefined;
}

const UserBadge: React.FC<UserBadgeProps> = ({ userId }) => {
    const dispatch = useAppDispatch();
    const userBadges = useRootState((state) => state.gamification.userBadges);
    const allBadges = useRootState((state) => state.gamification.badges);
    const isLoadingUserBadges = useRootState((state) => state.gamification.isLoadingUserBadges);
    const isLoadingAllBadges = useRootState((state) => state.gamification.isLoadingBadges);
    const [hoveredBadgeInfo, setHoveredBadgeInfo] = useState<{ name: string; requiredTasks: number } | null>(null);

    console.log("UserBadge rendered with userId:", userId);
    console.log("isLoadingUserBadges:", isLoadingUserBadges);
    console.log("isLoadingAllBadges:", isLoadingAllBadges);
    console.log("userBadges from Redux:", userBadges);
    console.log("allBadges from Redux:", allBadges);

    useEffect(() => {
        if (userId) {
            if (!allBadges || allBadges.length === 0) {
                dispatch(getAllBadgesAsync());
            }

            if (!userBadges || userBadges.length === 0) {
                dispatch(getAllBadgesByUserIdAsync(userId));
            }
        }
    }, [dispatch, userId]);



    const handleBadgeHover = (badge: IBadgeResponse) => {
        setHoveredBadgeInfo({ name: badge.name, requiredTasks: badge.requiredTasksToReceiveBadge });
    };

    const handleBadgeLeave = () => {
        setHoveredBadgeInfo(null);
    };

    return (
        <div className="user-badges-container">
            <div className="all-badges">
            <p className="badge-title">Badges:</p>
                {isLoadingAllBadges ? (
                    <p>Loading badges...</p>
                ) : allBadges && allBadges.length > 0 ? (
                    <div className="badges-grid">
                        {allBadges.map((badge) => {
                            const hasUserEarned = userBadges?.some((ub) => ub.badge.name === badge.name);
                            return (
                                <div
                                    key={badge.name}
                                    className={`badge-item ${!hasUserEarned ? 'locked' : ''}`}
                                    onMouseEnter={() => !hasUserEarned && handleBadgeHover(badge)}
                                    onMouseLeave={handleBadgeLeave}
                                >
                                    <img
                                        src={`${baseUrl}/images/badges/${badge.icon}.png`}
                                        alt={badge.name}
                                        className="badge-icon"
                                        style={{ filter: !hasUserEarned ? 'blur(5px)' : 'none' }}
                                    />
                                    {!hasUserEarned && <div className="lock-icon">🔒</div>}
                                </div>
                            );
                        })}
                    </div>
                ) : (
                    <p>No badges available.</p>
                )}
                {hoveredBadgeInfo && (
                    <div className="badge-info-tooltip">
                        To unlock "{hoveredBadgeInfo.name}", complete {hoveredBadgeInfo.requiredTasks} tasks.
                    </div>
                )}
            </div>
        </div>
    );
};

export default UserBadge;