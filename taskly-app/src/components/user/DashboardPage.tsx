import { Outlet } from 'react-router-dom';
import '../../styles/user/dashboard-style.scss';
import { MenuContainer } from '../general/MenuContainer';
import Logo from '../../assets/white_logo.png';
import { useRootState } from '../../redux/hooks';
import { useLayoutEffect, useState } from 'react';

export const DashboardPage = () => {
    const isAuthenticated = useRootState(s => s.authenticate.isAuthenticated);
    const [items, setItems] = useState<{
        name: string;
        path: string;
        sub_items: never[];
    }[]>([{
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
    {
        name: "Feedback",
        path: "/feedbacks",
        sub_items: []
    },
    ]);
    useLayoutEffect(() => {
        if (isAuthenticated === true) {
            setItems(prev => [...prev, {
                name: "Achievements",
                path: "/achievements",
                sub_items: []
            }]);
        }
        console.log("isAuthenticated - ", isAuthenticated);
        console.log("items - ", items);
    }, [isAuthenticated])
    return (<div className="dashboard-container">
        <MenuContainer icon={Logo} items={items} />
        <Outlet />
    </div>)
}