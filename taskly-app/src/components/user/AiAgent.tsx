import { useState } from "react";
import { api } from "../../axios/api.ts";
import ReactMarkdown from "react-markdown";
import { Prism as SyntaxHighlighter } from "react-syntax-highlighter";
import { oneDark } from "react-syntax-highlighter/dist/esm/styles/prism";
import "../../styles/ai/ai-main-style.scss";

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
            const result = await api.post("/api/gemini/generate", { prompt: prompt }, { withCredentials: true });
            setResponse(result.data);
        } catch (error) {
            console.error("Error generating content:", error);
        } finally {
            setLoading(false);
            setPrompt("");
        }
    };

    const copyToClipboard = () => {
        navigator.clipboard.writeText(response);
        alert("Code copied to clipboard!");
    };

    return (
        <div className="ai-container">
            {isContainerVisible && (
                <div className={`response-container ${loading ? 'loading' : ''} ${!loading && response ? 'visible' : ''}`}>
                    {loading ? (
                        <div className="loader-container">
                            <div className="loader"></div>
                        </div>
                    ) : (
                        <>
                            <ReactMarkdown
                                children={response}
                                components={{
                                    code({ node, inline, className, children, ...props }) {
                                        const match = /language-(\w+)/.exec(className || "");
                                        return !inline && match ? (
                                            <div className="code-block">
                                                <SyntaxHighlighter style={oneDark} language={match[1]} PreTag="div">
                                                    {String(children).replace(/\n$/, "")}
                                                </SyntaxHighlighter>
                                                <button className="copy-btn" onClick={copyToClipboard}>Copy</button>
                                            </div>
                                        ) : (
                                            <code className={className} {...props}>{children}</code>
                                        );
                                    }
                                }}
                            />
                        </>
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
                <button type="submit" className="ai-button" disabled={loading}>
                    {loading ? "Generating..." : "Generate"}
                </button>
            </form>
        </div>
    );
};

export default AIAgent;
