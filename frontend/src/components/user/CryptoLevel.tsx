import "../../styles/user/profile-style.scss"

export const CryptoLevel: React.FC<{ level: number }> = ({ level }) => (
    <div className="crypto-level-container">
        <svg width="60" height="30" viewBox="0 0 60 30">
            <defs>
                <linearGradient id="levelGradient" x1="0%" y1="0%" x2="100%" y2="0%">
                    <stop offset="0%" style={{ stopColor: "#e2ca2b", stopOpacity: 1 }} /> {/* Фіолетовий */}
                    <stop offset="100%" style={{ stopColor: "#ac8900", stopOpacity: 1 }} /> {/* Темно-фіолетовий */}
                </linearGradient>
                <filter id="glow" x="-50%" y="-50%" width="200%" height="200%">
                    <feGaussianBlur stdDeviation="2" result="blur" />
                    <feMerge>
                        <feMergeNode in="blur" />
                        <feMergeNode in="SourceGraphic" />
                    </feMerge>
                </filter>
            </defs>
            <rect width="60" height="30" rx="5" fill="url(#levelGradient)" filter="url(#glow)" />
            <text
                x="50%"
                y="50%"
                dominantBaseline="middle"
                textAnchor="middle"
                fontSize="16"
                fontWeight="bold"
                fill="#fff"
            >
                LVL {level}
            </text>
        </svg>
    </div>
);