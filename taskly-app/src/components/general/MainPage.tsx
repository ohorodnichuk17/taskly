import '../../styles/general/main-page-style.scss';
import {Link} from "react-router-dom";
import {useSelector} from "react-redux";
import {RootState} from "../../redux/store.ts";

export default function MainPage() {
    const { isLogin } = useSelector((state: RootState) => state.authenticate);

    return (
        <div className="main-page-container">
            <div className="text-content">
                <h1 className="gradient-text">
                    Collect, organize and solve problems from anywhere
                </h1>
                <p>
                    Forget clutter and chaos - increase your productivity with Taskly.
                </p>

                {!isLogin && (
                    <button>
                        <Link to="/authentication/register">
                            Register - it's FREE!
                        </Link>
                    </button>
                )}

            </div>

            <div className="animated-shapes">
                <svg className="shapes" viewBox="0 0 400 400" xmlns="http://www.w3.org/2000/svg">
                <circle cx="200" cy="200" r="150" fill="rgba(255, 255, 255, 0.1)" />
                    <circle cx="200" cy="200" r="100" fill="rgba(255, 255, 255, 0.05)" />
                    <circle cx="200" cy="200" r="50" fill="rgba(255, 255, 255, 0.2)">
                        <animate attributeName="r" values="50;70;50" dur="3s" repeatCount="indefinite" />
                    </circle>

                    <path
                        d="M100,200 C150,150 250,150 300,200 C250,250 150,250 100,200 Z"
                        fill="none"
                        stroke="rgba(255, 255, 255, 0.3)"
                        strokeWidth="2"
                    >
                        <animateTransform
                            attributeName="transform"
                            type="rotate"
                            from="0 200 200"
                            to="360 200 200"
                            dur="10s"
                            repeatCount="indefinite"
                        />
                    </path>

                    <circle cx="50" cy="50" r="5" fill="rgba(255, 255, 255, 0.8)" />
                    <circle cx="350" cy="350" r="5" fill="rgba(255, 255, 255, 0.8)" />
                    <circle cx="350" cy="50" r="5" fill="rgba(255, 255, 255, 0.8)" />
                    <circle cx="50" cy="350" r="5" fill="rgba(255, 255, 255, 0.8)" />
                </svg>
            </div>
        </div>
    );
}