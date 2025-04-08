import '../../styles/authentication/login-style.scss';
import { useAppDispatch } from '../../redux/hooks';
import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import password_hide from '../../../public/icon/password_hide.png';
import password_view from '../../../public/icon/password_view.png';
import { useForm } from 'react-hook-form';
import { LoginShema, LoginType } from '../../validation_types/types';
import { zodResolver } from '@hookform/resolvers/zod';
import { InputMessage, typeOfMessage } from '../general/InputMessage';
import { loginAsync } from '../../redux/actions/authenticateAction';
import { Loading } from '../general/Loading';

export const LoginPage = () => {
    const disptach = useAppDispatch();
    const navigate = useNavigate();
    const [passwordIsView, setPasswordIsView] = useState<boolean>(false);

    const {
        register,
        handleSubmit,
        setError,
        formState: {
            errors,
            isSubmitting
        }
    } = useForm<LoginType>({
        resolver: zodResolver(LoginShema),
        mode: "onChange"
    });
    const login = async (obj: LoginType) => {
        const response = await disptach(loginAsync({
            email: obj.email,
            password: obj.password,
            rememberMe: obj.rememberMe
        }));

        if (!loginAsync.fulfilled.match(response)) {
            if (response.payload) {
                setError("email", {
                    type: "custom",
                    message: response.payload.errors[0].code
                })
            }
            else {
                setError("email", {
                    type: "custom",
                    message: response.error.message || "Uncnown"
                })
            }
        }
        else {
            navigate("/");
        }
    }
    /*const testRequest = async () => {
        const response = await disptach(testMethod());

        if (!testMethod.fulfilled.match(response)) {
            if (response.payload) {
                console.log("Error - ", response.payload.errors[0].code);
            }
            else {
                console.log("Error - ", response.error.message || "Uncnown");
            }
        }
    }*/

    return (

        <div className='login-conatainer'>
            <form onSubmit={handleSubmit(login)}>
                {isSubmitting && <Loading />}
                <input
                    type="text"
                    placeholder='Email'
                    autoComplete='email'
                    {...register("email")}
                />
                {errors.email && errors.email.message && <InputMessage message={errors.email.message} typeOfMessage={typeOfMessage.error} />}
                <div className='password-container'>
                    <input
                        type={passwordIsView ? "text" : "password"}
                        placeholder='Password'
                        autoComplete='password'
                        {...register("password")}
                    />
                    <button className='password-button' onClick={(e) => {
                        e.preventDefault();
                        setPasswordIsView(!passwordIsView)
                    }}>
                        <img src={passwordIsView ? password_view : password_hide} />
                    </button>
                </div>
                {errors.password && errors.password.message && <InputMessage message={errors.password.message} typeOfMessage={typeOfMessage.error} />}
                <div className='remember-me-conteiner'>
                    <input
                        id='remember-me'
                        type='checkbox'
                        {...register("rememberMe")} />
                    <label htmlFor="remember-me">Remember me</label>
                </div>
                <div className='buttons'>
                    <button type='submit'>Login</button>
                    <button type='button' onClick={() => navigate("/authentication/forgot-password")}>Forgot password</button>
                    <Link to="/authentication/register">Don't have an account? Sign up</Link>
                </div>

            </form>
        </div>
    )
}
