import { useForm } from "react-hook-form";
import { useAppDispatch } from "../../redux/hooks";
import { EmailValidationShema, EmailValidationType } from "../../validation_types/types";
import { zodResolver } from "@hookform/resolvers/zod";
import { sendRequestToChangePasswordAsync } from "../../redux/actions/authenticateAction";
import { useNavigate } from "react-router-dom";
import { InputMessage, typeOfMessage } from "../general/InputMessage";
import { Loading } from "../general/Loading";
import "../../styles/authentication/forgot-password-style.scss";
import { TypeOfInformation } from "../../interfaces/generalInterface";
import { addInformation } from "../../redux/slices/generalSlice";

export const ForgotPasswordPage = () => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const { register,
        handleSubmit,
        setError,
        formState:
        { isSubmitting,
            errors }

    } = useForm<EmailValidationType>
            ({
                resolver: zodResolver(EmailValidationShema),
                mode: "onChange"
            });

    const sendRequestToChangePasswordHandler = async (obj: EmailValidationType) => {
        var response = await dispatch(sendRequestToChangePasswordAsync(obj.email));
        if (!sendRequestToChangePasswordAsync.fulfilled.match(response)) {
            if (response.payload) {
                setError("email", {
                    type: "custom",
                    message: response.payload.errors[0].code
                });
            }
            else {
                setError("email", {
                    type: "custom",
                    message: response.error.message
                });
            }
        } else {
            dispatch(addInformation({
                message: `Letter with confirmation of the change password has been send to ${obj.email}`,
                type: TypeOfInformation.Success
            }))
            navigate("/authentication/login")
        }

    }

    return (

        <div className='send-request-to-change-password-conatainer'>
            {isSubmitting == true && <Loading />}
            <p>Account email address</p>
            <form onSubmit={handleSubmit(sendRequestToChangePasswordHandler)}>
                <input {...register("email")} type="text" placeholder="Email" />
                {errors.email !== undefined ? <InputMessage typeOfMessage={typeOfMessage.error} message={errors.email.message!} /> : ""}
                <button type="submit">Send request</button>
            </form>
        </div>
    )
}