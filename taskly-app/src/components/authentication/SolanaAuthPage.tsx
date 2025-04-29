import '../../styles/solana/solana-auth-styles.scss';
import { WalletMultiButton } from '@solana/wallet-adapter-react-ui';
import { useDispatch } from "react-redux";
import { useRootState } from "../../redux/hooks.ts";
import { useWallet } from "@solana/wallet-adapter-react";
import { solanaWalletLoginAsync } from "../../redux/actions/solanaAuthAction.ts";
import { logout } from "../../redux/slices/solanaAuthSlice.ts";

const SolanaAuthPage = () => {
    const dispatch = useDispatch();
    const { token, loading, error, isAuthenticated } = useRootState((state) => state.solanaAuth);
    const { publicKey, connect, disconnect, wallet, connected } = useWallet();

    const handleAuthenticate = async () => {
        if (!wallet) {
            alert('No wallet detected. Please install a Solana wallet (e.g., Phantom).');
            return;
        }

        if (!publicKey) {
            try {
                await connect();
            } catch (error) {
                console.error('Failed to connect wallet:', error);
                alert('Failed to connect wallet. Please try again.');
                return;
            }
        }

        if (publicKey) {
            dispatch(solanaWalletLoginAsync(publicKey.toString()));
        } else {
            alert('Please connect your wallet first.');
        }
    };

    const handleLogout = () => {
        disconnect();
        dispatch(logout());
        alert('You have been logged out.');
    };

    return (
        <div className="solana-auth-page">
            {isAuthenticated ? (
                <div className="form-container">
                    <p className="wallet-info">Authenticated! Token: {token}</p>
                    <button onClick={handleLogout} className="logout-button">Logout</button>
                </div>
            ) : (
                <div className="form-container">
                    <h2>Solana Wallet Authentication</h2>
                    {connected ? (
                        <>
                            <p className="wallet-info">Connected Wallet: {publicKey?.toString()}</p>
                            <button onClick={handleAuthenticate} disabled={loading} className="auth-button">
                                Authenticate
                            </button>
                        </>
                    ) : (
                        <WalletMultiButton className="wallet-button" />
                    )}
                    {error && <p className="error-message">{error}</p>}
                </div>
            )}
        </div>
    );
};

export default SolanaAuthPage;
