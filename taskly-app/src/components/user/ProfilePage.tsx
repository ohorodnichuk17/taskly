import { baseUrl } from '../../axios/baseUrl';
import { useRootState } from '../../redux/hooks';
import '../../styles/user/profile-style.scss';

export const ProfilePage = () => {
    const userProfile = useRootState(s => s.authenticate.userProfile);
    return (<div className="profile-container">
        <div className="main-information">
            <div className='avatar'>
                <img src={baseUrl + "/images/avatars/" + userProfile?.avatarName + ".png"} alt="" />
            </div>
            <div className='name-and-buttons'>
                <p>{userProfile?.email}</p>
                <div className='buttons'>
                    <button>Edit profile</button>
                </div>
            </div>

        </div>
        <div className="profile-container-information"></div>
    </div>)
}