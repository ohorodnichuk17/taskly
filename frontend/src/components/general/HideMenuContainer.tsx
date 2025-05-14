import exit_icon from '../../assets/icon/exit_icon.png';
import { useRootState } from '../../redux/hooks';
import '../../styles/general/hide-menu-container-style.scss';
import { useState } from 'react';
import { Link } from 'react-router-dom';

export interface IHideMenuContainer {
    items: {
        name: string,
        path: string,
        sub_items: {
            name: string,
            path: string
        }[]
    }[],
    closeHideMenu: React.Dispatch<React.SetStateAction<boolean>>
}

export const HideMenuContainer = (props: IHideMenuContainer) => {

    const isLogin = useRootState(s => s.authenticate.isLogin);
    const [hideMenuAnimation, setHideMenuAnimation] = useState<string>("open-hide-menu")

    return (<div className={`hide-menu-container ${hideMenuAnimation}`}>
        <button className="close-button" onClick={async () => {

            setHideMenuAnimation("close-hide-menu");

            await new Promise(() => {
                setTimeout(() => {
                    props.closeHideMenu(false);
                }, 600)
            })
        }}>
            <img src={exit_icon} alt="Close hide menu" />
        </button>
        <nav className="hide-menu-items">
            {props.items.map((item, index) => (
                <Link
                    key={index}
                    className="menu-item"
                    to={item.path}
                >{item.name}</Link>
            ))}
        </nav>
        {isLogin == true ? <>

        </> :
            <div className='hide-menu-authentication-buttons'>
                <button>
                    <Link to="/authentication/login">Sign in</Link>
                </button>
                <button>
                    <Link to="/authentication/register">Sign up</Link>
                </button>
            </div>
        }
    </div>)
}