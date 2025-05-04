import '../../styles/authentication/login-style.scss';
import '../../styles/solana/solana-auth-styles.scss';

import { useAppDispatch, useRootState } from '../../redux/hooks';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { useNavigate } from 'react-router-dom';
import { WalletMultiButton } from '@solana/wallet-adapter-react-ui';
import { useWallet } from '@solana/wallet-adapter-react';
import { useState, useEffect } from 'react';

import {loginAsync, solanaWalletAuthAsync} from '../../redux/actions/authenticateAction';

import { LoginShema, LoginType } from '../../validation_types/types';
import { InputMessage, typeOfMessage } from '../general/InputMessage';
import { Loading } from '../general/Loading';

import password_hide from '../../../public/icon/password_hide.png';
import password_view from '../../../public/icon/password_view.png';

export const UnifiedLoginPage = () => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const [passwordIsView, setPasswordIsView] = useState(false);
    const [mode, setMode] = useState<'email' | 'solana'>('email');

    const { isAuthenticated } = useRootState(state => state.authenticate);
    const { publicKey, wallet, connected } = useWallet();

    const {
        register,
        handleSubmit,
        setError,
        formState: { errors }
    } = useForm<LoginType>({
        resolver: zodResolver(LoginShema),
        mode: 'onChange'
    });

    useEffect(() => {
        if (isAuthenticated) {
            navigate('/');
        }
    }, [isAuthenticated, navigate]);

    useEffect(() => {
        const handleAutoLogin = async () => {
            if (!wallet || !publicKey) return;

            try {
                const resultAction = await dispatch(solanaWalletAuthAsync(publicKey.toString()));
                if (!solanaWalletAuthAsync.fulfilled.match(resultAction)) {
                } else {
                    const userData = resultAction.payload;
                    if (userData.userName) {
                        navigate("/");
                    } else {
                        localStorage.setItem("user_profile_publicKey", publicKey.toString());
                        navigate("/authentication/set-username-solana");
                    }
                }
            } catch (error) {
                console.error("Error during Solana auto-login:", error);
            }
        };

        if (connected && publicKey) {
            handleAutoLogin();
        }
    }, [dispatch, connected, publicKey, wallet, navigate]);



    const login = async (obj: LoginType) => {
        const response = await dispatch(loginAsync({
            email: obj.email,
            password: obj.password,
            rememberMe: obj.rememberMe
        }));

        if (!loginAsync.fulfilled.match(response)) {
            const message = response.payload?.errors?.[0]?.code || response.error.message || "Unknown error";
            setError("email", { type: "custom", message });
        } else {
            navigate("/");
        }
    };

    return (
        <div className="login-conatainer">
            {isAuthenticated ? null : (
                <div className="form-container">
                    <div className="login-toggle">
                        <button
                            className={mode === 'email' ? 'active' : ''}
                            onClick={() => setMode('email')}
                        >
                            Email / Password
                        </button>
                        <button
                            className={mode === 'solana' ? 'active' : ''}
                            onClick={() => setMode('solana')}
                        >
                            Solana Wallet
                        </button>
                    </div>

                    {mode === 'email' ? (
                        <form onSubmit={handleSubmit(login)} className="email-form">
                            <input type="text" placeholder="Email" autoComplete="email" {...register("email")} />
                            {errors.email?.message &&
                                <InputMessage message={errors.email.message} typeOfMessage={typeOfMessage.error}/>}

                            <div className="password-container">
                                <input
                                    type={passwordIsView ? "text" : "password"}
                                    placeholder="Password"
                                    autoComplete="current-password"
                                    {...register("password")}
                                />
                                <button className="password-button" onClick={(e) => {
                                    e.preventDefault();
                                    setPasswordIsView(!passwordIsView);
                                }}>
                                    <img src={passwordIsView ? password_view : password_hide} alt="Toggle visibility"/>
                                </button>
                            </div>
                            {errors.password?.message &&
                                <InputMessage message={errors.password.message} typeOfMessage={typeOfMessage.error}/>}

                            <div className="remember-me-conteiner">
                                <input id="remember-me" type="checkbox" {...register("rememberMe")} />
                                <label htmlFor="remember-me">Remember me</label>
                            </div>

                            <div className="auth-actions">
                                <button type="submit">Login</button>
                                <div className="auth-links">
                                    <span onClick={() => navigate("/authentication/forgot-password")}>
                                        Forgot password?
                                    </span>
                                    <span onClick={() => navigate("/authentication/register")}>
                                        Create account
                                    </span>
                                </div>
                            </div>
                        </form>
                    ) : (
                        <div className="solana-auth-section">
                            {connected ? (
                                <div className="auto-login-loader">
                                    <Loading />
                                </div>
                            ) : (
                                <WalletMultiButton />
                            )}
                        </div>
                    )}
                </div>
            )}
        </div>
    );
};