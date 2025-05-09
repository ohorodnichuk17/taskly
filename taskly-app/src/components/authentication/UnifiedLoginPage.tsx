import '../../styles/authentication/login-style.scss';
import '../../styles/solana/solana-auth-styles.scss';

import { useAppDispatch, useRootState } from '../../redux/hooks';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { useNavigate } from 'react-router-dom';
import { WalletMultiButton } from '@solana/wallet-adapter-react-ui';
import { useWallet } from '@solana/wallet-adapter-react';
import { useState, useEffect } from 'react';

import { loginAsync, solanaWalletAuthAsync } from '../../redux/actions/authenticateAction';

import { LoginShema, LoginType, UseReferralCodeShema, UseRefferalCodeType } from '../../validation_types/types';
import { InputMessage, typeOfMessage } from '../general/InputMessage';
import { Loading } from '../general/Loading';

import password_hide from '../../assets/icon/password_hide.png';
import password_view from '../../assets/icon/password_view.png';
import { setRefferalCode } from '../../redux/slices/authenticateSlice';

export const UnifiedLoginPage = () => {
   const dispatch = useAppDispatch();
   const navigate = useNavigate();

   const [passwordIsView, setPasswordIsView] = useState(false);
   const [mode, setMode] = useState<'email' | 'solana'>('email');

   const { isAuthenticated, referralCode } = useRootState(state => state.authenticate);
   const { publicKey, wallet, connected } = useWallet();

   const {
      register: registerLogin,
      handleSubmit: handleSubmitLogin,
      setError,
      formState: { errors: errorsLogin }
   } = useForm<LoginType>({
      resolver: zodResolver(LoginShema),
      mode: 'onChange'
   });

   const {
      register: registerReferral,
      handleSubmit: handleSubmitReferral,
      formState: { errors: errorsReferral }
   } = useForm<UseRefferalCodeType>({
      resolver: zodResolver(UseReferralCodeShema),
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
            const resultAction = await dispatch(solanaWalletAuthAsync({
               publicKey: publicKey.toString(),
               referralCode: referralCode
            }));
            if (!solanaWalletAuthAsync.fulfilled.match(resultAction)) {
            } else {
               dispatch(setRefferalCode(null));
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

   const useReferralCodeSubmit = (obj: UseRefferalCodeType) => {
      dispatch(setRefferalCode(obj.referralCode));
   }


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
                  <form onSubmit={handleSubmitLogin(login)} className="email-form">
                     <input type="text" placeholder="Email" autoComplete="email" {...registerLogin("email")} />
                     {errorsLogin.email?.message &&
                        <InputMessage message={errorsLogin.email.message} typeOfMessage={typeOfMessage.error} />}

                     <div className="password-container">
                        <input
                           type={passwordIsView ? "text" : "password"}
                           placeholder="Password"
                           autoComplete="current-password"
                           {...registerLogin("password")}
                        />
                        <button className="password-button" onClick={(e) => {
                           e.preventDefault();
                           setPasswordIsView(!passwordIsView);
                        }}>
                           <img src={passwordIsView ? password_view : password_hide} alt="Toggle visibility" />
                        </button>
                     </div>
                     {errorsLogin.password?.message &&
                        <InputMessage message={errorsLogin.password.message} typeOfMessage={typeOfMessage.error} />}

                     <div className="remember-me-conteiner">
                        <input id="remember-me" type="checkbox" {...registerLogin("rememberMe")} />
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
                     ) : (<>
                        <form
                           className='use-referral-code'
                           onSubmit={handleSubmitReferral(useReferralCodeSubmit)}>
                           <input className='use-referral-input' maxLength={6} type="text" {...registerReferral("referralCode")} />
                           <button className='use-referral-button'>Use Refferal</button>
                        </form>
                        {errorsReferral.referralCode?.message &&
                           <InputMessage message={errorsReferral.referralCode.message} typeOfMessage={typeOfMessage.error} />}
                        <WalletMultiButton />
                     </>
                     )}
                  </div>
               )}
            </div>
         )}
      </div>
   );
};