import { Outlet } from 'react-router-dom';
import '../../styles/user/dashboard-style.scss';
import { MenuContainer } from '../general/MenuContainer';
import Logo from '../../assets/white_logo.png';

export const DashboardPage = () => {
    return (<div className="dashboard-container">
        <MenuContainer icon={Logo} items={[{
            name: "Home",
            path: "/",
            sub_items: []
        },
        {
            name: "Boards",
            path: "/boards",
            sub_items: []
        },
        {
            name: "Tables",
            path: "/tables",
            sub_items: []
        },
            {
                name: "AI Agent",
                path: "/artificial-intelligence",
                sub_items: []
            },
        ]} />
        <Outlet />
    </div>)
}