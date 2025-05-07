import "../../styles/achievements/achievements-style.scss";
import { useEffect, useLayoutEffect, useRef, useState } from "react";
import { getAllAchievementsAsync } from "../../redux/actions/achievementsActions";
import { useAppDispatch, useRootState } from "../../redux/hooks"
import { IAchievement } from "../../interfaces/achievementsInterface";
import { Achievement } from "./Achievement";

export const AchievementsPage = () => {

    const achievementsScrollbarRef = useRef<HTMLDivElement | null>(null)

    const [achievementsList, setAchievementsList] = useState<IAchievement[] | null>(null);
    const [achievementsListOverflowY, setAchievementsListOverflowY] = useState<"auto" | "scroll">("auto");

    const achievements = useRootState(s => s.achievements.achievements);
    const dispatch = useAppDispatch();

    const getAchievements = async () => {
        await dispatch(getAllAchievementsAsync());
    }
    useEffect(() => {
        getAchievements();
    }, [])
    useEffect(() => {
        if (achievements) {
            setAchievementsList(achievements);
            console.log(achievements)
        }
    }, [achievements])

    return (<div className="achievements-container">
        <h2 className="gradient-text">ACHIEVEMENTS</h2>
        <div className="achievements-scrollbar"
            ref={(ref) => {
                achievementsScrollbarRef.current = ref;
            }}
            style={{ overflowY: achievementsListOverflowY }}
            onResize={() => {
                if (achievementsScrollbarRef.current && achievementsScrollbarRef.current.offsetHeight < achievementsScrollbarRef.current.scrollHeight) {
                    setAchievementsListOverflowY("scroll");
                }
                else {
                    if (achievementsListOverflowY !== "auto")
                        setAchievementsListOverflowY("auto");
                }
            }}
        >
            <div className="achievements-list">
                {achievementsList && achievementsList.map((achievement) => (
                    <Achievement
                        //key={achievement.id}
                        id={achievement.id}
                        name={achievement.name}
                        reward={achievement.reward}
                        description={achievement.description}
                        percentageOfCompletion={achievement.percentageOfCompletion}
                        icon={achievement.icon}
                        isCompleated={achievement.isCompleated}
                    />
                ))}
            </div>

        </div>
    </div>)
}