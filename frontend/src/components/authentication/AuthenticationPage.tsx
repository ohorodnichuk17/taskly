import { Outlet } from 'react-router-dom';
import '../../styles/authentication/authentication-style.scss';
import { useRootState } from '../../redux/hooks';
import { InformationAlert } from '../general/InformationAlert';
import logo from "../../assets/white_logo.png";

export const AuthenticationPage = () => {
    const information = useRootState((s) => s.general.information);

    return (
        <div className="authentication-page">
            {information && (
                <InformationAlert message={information.message} type={information.type} />
            )}
            <div className="authentication-container">
                <div className="authentication-header">
                    <img
                        src={logo}
                        alt="Taskly Logo"
                        className="authentication-logo"
                    />
                    <h1 className="authentication-title">Taskly ToDo</h1>
                </div>
                <Outlet />
            </div>
        </div>
    );
};
/*

*/ 