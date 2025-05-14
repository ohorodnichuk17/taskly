import { useEffect } from "react";
import { baseUrl } from "../../axios/baseUrl";
import "../../styles/achievements/achievement-alert-style.scss";
import { useAppDispatch } from "../../redux/hooks";
import { setNewAchievement } from "../../redux/slices/achievementsSlice";

interface IAchievementAlert {
    name: string,
    icon: string
}

export const AchievementAlert = (props: IAchievementAlert) => {
    const dispatch = useAppDispatch();
    const closeAlert = async () => {
        await new Promise(() => {
            setTimeout(() => {
                dispatch(setNewAchievement(null));
            }, 4000);
        })
    }
    useEffect(() => {
        closeAlert();
    }, [])
    return (<div id="achievement-alert">
        <div className="achievement-alert-icon">
            <img src={`${baseUrl}/images/achievements/${props.icon}.png`} alt="New achievement icon" />
        </div>
        <p>Have received a new achievement</p>
        <p>{props.name}</p>
    </div>)
}