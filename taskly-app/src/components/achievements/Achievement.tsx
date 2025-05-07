import "../../styles/achievements/achievement-style.scss";
import { baseUrl } from "../../axios/baseUrl"
import solana_icon from "../../assets/icon/solana_icon.png";
import { IAchievement } from "../../interfaces/achievementsInterface";
import lock_icon from "../../assets/icon/lock_icon.png";



export const Achievement = (props: IAchievement) => {

    return (<div id="achievement">
        {props.isCompleated === false &&
            <div id="achievement-is-not-compleated">
                <img src={lock_icon} alt="Lock icon" />
            </div>}
        <div id="achievement-icon">
            <img src={`${baseUrl}/images/achievements/${props.icon}.png`} alt="Achievement icon" />
        </div>

        <div id="achievement-name-and-reward">
            <p>{props.name}</p>
            <p>{props.reward.toString()}</p>
            <img src={solana_icon} alt="Solana icon" />
        </div>
        <div id="achievement-description">
            <p>{props.description}</p>
        </div>
        <div id="achievement-percentage-of-completion">
            <p>This achievement was achieved by {props.percentageOfCompletion}% of users</p>
        </div>
    </div>)
}