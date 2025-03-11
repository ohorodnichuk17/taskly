import { useState } from "react";
import { api } from "../../axios/api.ts";
import '../../styles/ai/ai-main-style.scss';

const AIAgent = () => {
    const [prompt, setPrompt] = useState("");
    const [response, setResponse] = useState("");
    const [loading, setLoading] = useState(false);
    const [isContainerVisible, setIsContainerVisible] = useState(false);

    const handleInputChange = (e) => {
        setPrompt(e.target.value);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!prompt.trim()) return;

        setLoading(true);
        setIsContainerVisible(true);
        setResponse("");

        try {
            const result = await api.post("/api/gemini/generate", { prompt: prompt });
            setResponse(result.data);
        } catch (error) {
            console.error("Error generating content:", error);
        } finally {
            setLoading(false);
            setPrompt("");
        }
    };

    return (
        <div className="ai-container">
            <h1 className="ai-title">AI Agent</h1>

            {isContainerVisible && (
                <div className={`response-container ${loading ? 'loading' : ''} ${!loading && response ? 'visible' : ''}`}>
                    {loading ? (
                        <div className="loader"></div>
                    ) : (
                        <p className="ai-response">{response}</p>
                    )}
                </div>
            )}

            <form className="ai-form" onSubmit={handleSubmit}>
                <input
                    type="text"
                    value={prompt}
                    onChange={handleInputChange}
                    placeholder="Enter your request..."
                    className="ai-input"
                />
                <button type="submit" className="ai-button">Generate</button>
            </form>
        </div>
    );
};

export default AIAgent;