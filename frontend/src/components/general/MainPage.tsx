import '../../styles/general/main-page-style.scss';
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "../../redux/store.ts";
import solanaNft from "../../assets/nft/solana-nft-sample.png"
import nft1 from "../../assets/nft/nft1.png"
import nft2 from "../../assets/nft/nft2.png"
import nft3 from "../../assets/nft/nft3.png"

export default function MainPage() {
    const { isLogin } = useSelector((state: RootState) => state.authenticate);

    return (
        <div className="main-page-container">
            <section className="hero-section">
                <div className="text-content">
                    <h1 className="gradient-text">
                        Create. Thrive. Organize.
                    </h1>
                    <p className="subtitle">
                        Your gateway to smarter project management — enhanced with Solana NFTs.
                    </p>

                    {!isLogin && (
                        <button className="primary-button">
                            <Link to="/authentication/register">
                                Get Started — Free
                            </Link>
                        </button>
                    )}
                </div>

                <div className="hero-visual">
                    <img src={solanaNft} alt="Solana NFTs" className="floating-image" />
                </div>
            </section>

            <section className="features-section">
                <div className="feature-card" data-aos="fade-up">
                    <h2>Seamless Solana Integration</h2>
                    <p>Manage tasks with verified NFT ownership. Authenticate effortlessly and stand out.</p>
                </div>

                <div className="feature-card" data-aos="fade-up" data-aos-delay="200">
                    <h2>Blazing Fast Performance</h2>
                    <p>Enjoy lightning-speed updates powered by decentralized technologies.</p>
                </div>

                <div className="feature-card" data-aos="fade-up" data-aos-delay="400">
                    <h2>Efficient Performance</h2>
                    <p>Experience fast load times and smooth interactions, even with complex data and heavy
                        processes.</p>
                </div>
            </section>

            <section className="nft-gallery" data-aos="zoom-in">
                <h2 className="gallery-title">Discover Exclusive NFTs</h2>
                <div className="nft-grid">
                    <img src={nft1} alt="NFT 1" />
                    <img src={nft2} alt="NFT 2" />
                    <img src={nft3} alt="NFT 3" />
                </div>
            </section>
        </div>
    );
}
