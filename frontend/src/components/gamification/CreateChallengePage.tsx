import { useState } from "react";
import { useAppDispatch } from "../../redux/hooks.ts";
import { useNavigate } from "react-router-dom";
import { createChallengeAsync } from "../../redux/actions/gamificationAction.ts";
import "../../styles/challenge/create-challenge-styles.scss";
import {ICreateChallengeRequest} from "../../interfaces/gamificationInterface.ts";

export default function CreateChallengePage() {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const [name, setName] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [startDate, setStartDate] = useState<string>("");
    const [endDate, setEndDate] = useState("");
    const [points, setPoints] = useState<number>(0);
    const [isActive, setIsActive] = useState<boolean>(false);
    const [ruleKey, setRuleKey] = useState<string>("Taskly:CompletedTableItems");
    const [targetAmount, setTargetAmount] = useState<number>(1);
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false);

    const handleSubmit = async () => {
        if (!name || !description || !startDate || !endDate || points <= 0 || targetAmount <= 0) {
            setError("Please fill in all required fields with valid values.");
            return;
        }
        const formattedStartTime = new Date(startDate);
        const formattedEndTime = new Date(endDate);

        const request: ICreateChallengeRequest = {
            name: name,
            description: description,
            startTime: formattedStartTime.toISOString(),
            endTime: formattedEndTime.toISOString(),
            points: points,
            isActive: isActive,
            ruleKey: ruleKey,
            targetAmount: targetAmount,
        };


        setIsLoading(true);
        setError(null);

        try {
            await dispatch(createChallengeAsync(request));
            navigate("/challenges");
        } catch (err: any) {
            if (typeof err === "object" && err.errors) {
                setError(Object.values(err.errors).join(" "));
            } else {
                setError("Failed to create challenge.");
            }
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="challenge-create-page">
            <header className="challenge-create-header">
                <h1 className="gradient-text">Create New Challenge</h1>
            </header>

            <form className="challenge-form" onSubmit={(e) => e.preventDefault()}>
                <div className="form-group">
                    <label htmlFor="name">Challenge Name</label>
                    <input
                        id="name"
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        placeholder="Enter challenge name"
                        className="challenge-input"
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="description">Description</label>
                    <textarea
                        id="description"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        placeholder="Describe the challenge"
                        className="challenge-textarea"
                    />
                </div>

                <div className="form-row">
                    <div className="form-group">
                        <label htmlFor="startDate">Start Date</label>
                        <input
                            id="startDate"
                            type="date"
                            value={startDate}
                            onChange={(e) => setStartDate(e.target.value)}
                            className="challenge-input"
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="endDate">End Date</label>
                        <input
                            id="endDate"
                            type="date"
                            value={endDate}
                            onChange={(e) => setEndDate(e.target.value)}
                            className="challenge-input"
                        />
                    </div>
                </div>

                <div className="form-row">
                    <div className="form-group">
                        <label htmlFor="points">Points</label>
                        <input
                            id="points"
                            type="number"
                            min={1}
                            value={points}
                            onChange={(e) => setPoints(parseInt(e.target.value))}
                            className="challenge-input"
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="targetAmount">Target Amount</label>
                        <input
                            id="targetAmount"
                            type="number"
                            min={1}
                            value={targetAmount}
                            onChange={(e) => setTargetAmount(parseInt(e.target.value))}
                            className="challenge-input"
                        />
                    </div>
                </div>

                <div className="form-group">
                    <label htmlFor="ruleKey">Rule Key</label>
                    <select
                        id="ruleKey"
                        value={ruleKey}
                        onChange={(e) => setRuleKey(e.target.value)}
                        className="challenge-select"
                    >
                        <option value="Taskly:CompletedTableItems">Completed Table Items</option>
                        <option value="Taskly:CountUserFeedbacks">User Feedbacks</option>
                        <option value="Taskly:CountUserReferrals">User Referrals</option>
                    </select>
                </div>

                <div className="form-group checkbox-group">
                    <label className="challenge-checkbox">
                        <input
                            type="checkbox"
                            checked={isActive}
                            onChange={(e) => setIsActive(e.target.checked)}
                        />
                        Active
                    </label>
                </div>

                <button
                    onClick={handleSubmit}
                    className="challenge-create-btn"
                    disabled={isLoading}
                >
                    {isLoading ? "Creating..." : "Create Challenge"}
                </button>

                {error && <p className="challenge-error">{error}</p>}
            </form>
        </div>
    );
}