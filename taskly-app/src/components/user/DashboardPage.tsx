import { Outlet } from 'react-router-dom';
import '../../styles/user/dashboard-style.scss';
import { MenuContainer } from '../general/MenuContainer';
import taskly_todo_icon from '../../../public/icon/taskly_todo_icon.png';

export const DashboardPage = () => {
    return (<div className="dashboard-container">
        <MenuContainer icon={taskly_todo_icon} items={[{
            name: "Home",
            sub_items: []
        },
        {
            name: "Boards",
            sub_items: []
        },
        {
            name: "Tables",
            sub_items: []
        }
        ]} />
        <Outlet />
    </div>)
}