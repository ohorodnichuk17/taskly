
import '../../styles/authentication/register-style.scss';
import { SendVerificationCodePage } from './SendVerificationCodePage';
import { VerificateEmailPage } from './VerificateEmailPage';
import { useRootState } from '../../redux/hooks';
import { FinalRegisterPage } from './FinalRegisterPage';



export const RegisterPage = () => {
    const emailVerification = useRootState(s => s.authenticateReducer.verificationEmail);
    const emailVerificated = useRootState(s => s.authenticateReducer.verificatedEmail);

    const renderRegister = () => {
        if (emailVerification == null) {
            return <SendVerificationCodePage />;
        }
        if (emailVerificated == null) {
            return <VerificateEmailPage />;
        }
        return <FinalRegisterPage />;
    }
    return (<div className='register-page'>
        <div className='register-conatainer'>
            <div className='register-conatainer-name'>Taskly ToDo</div>
            {renderRegister()}
        </div>
    </div>)
}
