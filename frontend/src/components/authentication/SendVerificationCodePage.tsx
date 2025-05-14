import { useForm } from 'react-hook-form';
import { EmailValidationShema, EmailValidationType } from '../../validation_types/types';
import { zodResolver } from "@hookform/resolvers/zod";
import "../../styles/authentication/send-verification-code-style.scss";

import { useAppDispatch } from '../../redux/hooks';
import { sendVerificationCodeAsync } from '../../redux/actions/authenticateAction';

import { InputMessage, typeOfMessage } from '../general/InputMessage';
import { Loading } from '../general/Loading';


export const SendVerificationCodePage = () => {
    const dispatch = useAppDispatch();
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

    const verificateEmail = async (obj: EmailValidationType) => {
        var response = await dispatch(sendVerificationCodeAsync(obj.email));
        if (!sendVerificationCodeAsync.fulfilled.match(response)) {
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
        }

    }

    return (
        <div className='send-verification-code-conatainer'>
            {isSubmitting == true && <Loading />}
            <p>Email address verification</p>
            <form onSubmit={handleSubmit(verificateEmail)}>
                <input {...register("email")} type="text" placeholder="Email" />
                {errors.email !== undefined ? <InputMessage typeOfMessage={typeOfMessage.error} message={errors.email.message!} /> : ""}
                <button type="submit">Send code</button>
            </form>
        </div>
    )
}