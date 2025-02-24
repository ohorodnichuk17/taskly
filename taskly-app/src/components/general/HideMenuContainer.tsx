import { resolve } from 'path';
import exit_icon from '../../../public/icon/exit_icon.png';
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

    const [hideMenuAnimation, setHideMenuAnimation] = useState<string>("open-hide-menu")

    return (<div className={`hide-menu-container ${hideMenuAnimation}`}>
        <button className="close-button" onClick={async () => {

            setHideMenuAnimation("close-hide-menu");

            await new Promise((resolve) => {
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
    </div>)
}