import { useForm } from "react-hook-form";
import { PasswordValidationShema, PasswordValidationType } from "../../validation_types/types";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import too_weak_password from '../../../public/icon/too_weak_icon.png';
import weak_password from '../../../public/icon/weak_icon.png';
import medium_password from '../../../public/icon/medium_icon.png';
import strong_password from '../../../public/icon/strong_icon.png';
import password_hide from '../../../public/icon/password_hide.png';
import password_view from '../../../public/icon/password_view.png';
import { passwordStrength } from "check-password-strength";
import { InputMessage, typeOfMessage } from "../general/InputMessage";
import { Loading } from "../general/Loading";
import { Navigate, useNavigate, useParams } from "react-router-dom";
import { useAppDispatch, useRootState } from "../../redux/hooks";
import "../../styles/authentication/change-password-style.scss";
import { changePasswordAsync, checkHasUserSentRequestToChangePasswordAsync } from "../../redux/actions/authenticateAction";
import { IChangePasswordRequest } from "../../interfaces/authenticateInterfaces";

export const ChangePasswordPage = () => {
    const emailOfUserWhoWantToChangePassword = useRootState(s => s.authenticate.emailOfUserWhoWantToChangePassword);
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const [passwordIsView, setPasswordIsView] = useState<boolean>(false);
    const [confirmPasswordIsView, setConfirmPasswordIsView] = useState<boolean>(false);
    const [password, setPassword] = useState<string>("");
    const [loading, setLoading] = useState(true);
    const { key } = useParams();

    const passwordStrengthColors = [
        "red",
        "orange",
        "yellow",
        "green"];

    const passwordStrengthIcons = [
        too_weak_password,
        weak_password,
        medium_password,
        strong_password];

    const {
        register,
        handleSubmit,
        trigger,
        formState: {
            errors,
            isSubmitting
        },
        setError
    } = useForm<PasswordValidationType>({
        resolver: zodResolver(PasswordValidationShema),
        mode: "onChange"
    })
    const checkHasUserSentRequest = async () => {
        setLoading(true);
        await dispatch(checkHasUserSentRequestToChangePasswordAsync({
            key: key!
        }));
        setLoading(false);
    }
    const getPasswordStrength = (password: string) => {
        const strength = passwordStrength(password).id;
        return strength;
    }

    useEffect(() => {
        checkHasUserSentRequest();
    }, [key])


    const changePasswordHandler = async (obj: PasswordValidationType) => {
        var request: IChangePasswordRequest = {
            email: emailOfUserWhoWantToChangePassword!,
            password: obj.password,
            confirmPassword: obj.confirmPassword
        };

        var response = await dispatch(changePasswordAsync(request));

        if (!changePasswordAsync.fulfilled.match(response)) {
            if (response.payload) {
                setError("password", {
                    type: "custom",
                    message: response.payload.errors[0].code
                });
            }
            else {
                setError("password", {
                    type: "custom",
                    message: response.error.message
                });
            }
        }
        else {
            navigate("/authentication/login");
        }
    };

    if (loading) {
        return <Loading />;
    }
    else if (emailOfUserWhoWantToChangePassword === null) {
        return <Navigate to="/not-found" />
    }
    else
        return (
            <div className="change-password-container">
                {isSubmitting == true && <Loading />}
                <p>Change password</p>
                <form onSubmit={handleSubmit(changePasswordHandler)}>
                    <div className='password-container'>
                        <input
                            {...register("password")}

                            type={passwordIsView ? "text" : "password"}
                            placeholder='Password'
                            autoComplete='password'
                            onChange={async (e) => {
                                setPassword(e.target.value);
                                register("password").onChange(e);
                                await trigger();

                            }}
                        />
                        <button className='password-button' onClick={(e) => {
                            e.preventDefault();
                            setPasswordIsView(!passwordIsView)
                        }}>
                            <img src={passwordIsView ? password_view : password_hide} />
                        </button>
                        <div className='password-strength' style={{ backgroundColor: passwordStrengthColors[getPasswordStrength(password)] }}>
                            <img src={passwordStrengthIcons[getPasswordStrength(password)]} alt="" />
                        </div>

                    </div>
                    {errors.password?.message && <InputMessage message={errors.password.message} typeOfMessage={typeOfMessage.error} />}
                    <div className='password-container'>
                        <input
                            {...register("confirmPassword")}
                            type={confirmPasswordIsView ? "text" : "password"}
                            placeholder='Confirm password'
                            autoComplete='confirm-password'
                        />
                        <button className='password-button' onClick={(e) => {
                            e.preventDefault();
                            setConfirmPasswordIsView(!confirmPasswordIsView)
                        }}>
                            <img src={confirmPasswordIsView ? password_view : password_hide} />
                        </button>
                    </div>
                    {errors.confirmPassword?.message && <InputMessage message={errors.confirmPassword.message} typeOfMessage={typeOfMessage.error} />}
                    <button type='submit'>Register</button>
                </form>
            </div>

        )
}