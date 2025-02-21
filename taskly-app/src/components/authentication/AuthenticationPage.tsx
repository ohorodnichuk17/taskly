import { Outlet } from 'react-router-dom';
import '../../styles/authentication/authentication-style.scss';
import { useRootState } from '../../redux/hooks';
import { InformationAlert } from '../general/InformationAlert';
import Logo from '../../assets/white_logo.png';

export const AuthenticationPage = () => {
    const information = useRootState((s) => s.general.information);

    return (
        <div className="authentication-page">
            <div className="authentication-container">
                {information && (
                    <InformationAlert message={information.message} type={information.type}/>
                )}
                <div className="authentication-header">
                    <img
                        src={Logo}
                        alt="Taskly Logo"
                        className="authentication-logo"
                    />
                    <h1 className="authentication-title">Taskly ToDo</h1>
                </div>
                <Outlet/>
            </div>
        </div>
    );
};