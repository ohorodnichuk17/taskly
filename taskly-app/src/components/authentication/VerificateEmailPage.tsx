import { useForm } from 'react-hook-form';
import { EmailVerificationShema, EmailVerificationType } from '../../validation_types/types';
import { zodResolver } from "@hookform/resolvers/zod";

import { useAppDispatch, useRootState } from '../../redux/hooks';
import { InputMessage, typeOfMessage } from '../general/InputMessage';
import { InputForCode } from '../general/InputForCode';
import '../../styles/authentication/verificate-email-style.scss';
import { useState } from 'react';
import { sendVerificationCodeAsync, verificateEmailAsync } from '../../redux/actions/authenticateAction';
import { Loading } from '../general/Loading';


export const VerificateEmailPage = () => {
    const emailVerification = useRootState(s => s.authenticateReducer.verificationEmail);
    const [lastInput, setLastInput] = useState<HTMLInputElement | null>(null);
    const dispatch = useAppDispatch();
    const { register,
        handleSubmit,
        setError,
        formState:
        { isSubmitting,
            errors }

    } = useForm<EmailVerificationType>
            ({
                defaultValues: {
                    email: emailVerification || "",
                    code: Array(7).fill(""),
                },
                resolver: zodResolver(EmailVerificationShema),
                mode: "onChange"
            });

    const verificateEmail = async (obj: EmailVerificationType) => {
        lastInput?.focus();
        const response = await dispatch(verificateEmailAsync(
            {
                email: obj.email,
                code: obj.code.join("")
            }));

        if (!verificateEmailAsync.fulfilled.match(response)) {
            if (response.payload) {
                setError("email", {
                    message: response.payload.errors[0].code
                })
            }
            else {
                setError("email", {
                    message: response.error.message
                })
            }
        }

    }

    return (
        <div className='verificate-email-conatainer'>
            {isSubmitting == true && <Loading />}
            <p>Verification code has been receive to {emailVerification}</p>
            <form onSubmit={handleSubmit(verificateEmail)}>
                <InputForCode register={register} lastInput={setLastInput} />
                {errors.email !== undefined ? <InputMessage typeOfMessage={typeOfMessage.error} message={errors.email.message!} /> : ""}
                {Array.isArray(errors.code) && errors.code.find(el => el !== undefined) ? <InputMessage typeOfMessage={typeOfMessage.error} message={errors.code.find(el => el !== undefined).message!} /> : ""}
                <div className='buttons'>
                    <button type="submit">Verificate Email</button>
                    <button type="button" onClick={async () => {
                        var response = await dispatch(sendVerificationCodeAsync(emailVerification || ""));
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
                    }}>Resend the code</button>
                </div>


            </form>
        </div>
    )
}
//