import { Outlet } from 'react-router-dom';
import '../../styles/authentication/authentication-style.scss';
import { useRootState } from '../../redux/hooks';
import { InformationAlert } from '../general/InformationAlert';
export const AuthenticationPage = () => {
    const information = useRootState(s => s.general.information);


    return (<div className='authentication-page'>
        <div className='authentication-conatainer'>
            {information && <InformationAlert message={information.message} type={information.type} />}
            <div className='authentication-conatainer-name'>Taskly ToDo</div>
            <Outlet />


        </div>
    </div>)
}