import { Outlet } from 'react-router-dom';
import '../../styles/user/dashboard-style.scss';
import { MenuContainer } from '../general/MenuContainer';
import Logo from '../../assets/white_logo.png';
import { useRootState } from '../../redux/hooks';
import { useLayoutEffect, useState } from 'react';
import { AchievementAlert } from '../achievements/AchievementAlert';

export const DashboardPage = () => {
    const isAuthenticated = useRootState(s => s.authenticate.isAuthenticated);
    const newAchievement = useRootState(s => s.achievements.newAchievement);
    const userProfile = useRootState(s => s.authenticate.userProfile);

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
        const showChallenges = isAuthenticated === true || userProfile?.email === "tasklytodolist@gmail.com";

        if (showChallenges) {
            if (!items.find(i => i.name === "Challenges")) {
                setItems(prev => [...prev, {
                    name: "Challenges",
                    path: "/challenges",
                    sub_items: []
                }]);
            }
            if (!items.find(i => i.name === "Achievements") && isAuthenticated === true) {
                setItems(prev => [...prev, {
                    name: "Achievements",
                    path: "/achievements",
                    sub_items: []
                }]);
            }
        } else {
            const challengesIndex = items.findIndex(a => a.name === "Challenges");
            if (challengesIndex !== -1) {
                setItems(prev => prev.filter(item => item.name !== "Challenges"));
            }
            const achievementsIndex = items.findIndex(a => a.name === "Achievements");
            if (achievementsIndex !== -1) {
                setItems(prev => prev.filter(item => item.name !== "Achievements"));
            }
        }
    }, [isAuthenticated, userProfile?.email, items]);
    return (<div className="dashboard-container">
        {newAchievement !== null &&
            <AchievementAlert
                name={newAchievement.name}
                icon={newAchievement.icon}
            />
        }
        <MenuContainer icon={Logo} items={items} />
        <Outlet />
    </div>)
}