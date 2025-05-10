import { useEffect, useLayoutEffect, useRef, useState } from 'react';
import '../../styles/general/menu-container-style.scss';
import menu_icon from '../../assets/icon/menu_icon.png';
import menu_icon_opened from '../../assets/icon/menu_icon_opened.png';
import {Link, Navigate, useNavigate} from 'react-router-dom';
import { HideMenuContainer } from './HideMenuContainer';
import {logoutAsync, solanaLogoutAsync} from "../../redux/actions/authenticateAction.ts";
import {baseUrl} from "../../axios/baseUrl.ts";
import {useAppDispatch, useRootState} from "../../redux/hooks.ts";
import {getAllBadgesByUserIdAsync} from "../../redux/actions/gamificationAction.ts";

export interface IMenuContainer {
    icon: string;
    items: {
        name: string,
        path: string,
        sub_items: {
            name: string,
            path: string
        }[]
    }[]
}

function CalculateMenuItemsWidth(items: (HTMLAnchorElement | null)[]) {
    if (items === null) return 0;

    let itemsLength = 0;
    items.forEach(element => {
        if (element) {
            itemsLength += element.offsetWidth;
        }
    });
    itemsLength += (items.length - 1) * 20 + 100;
    return itemsLength;
}

export const MenuContainer = (props: IMenuContainer) => {
    const { isLogin, userProfile, solanaUserProfile, authMethod } = useRootState(state => state.authenticate);
    const widthToHide = useRef<number | null>(null);
    const containerRef = useRef<HTMLDivElement | null>();
    const containerItemsRef = useRef<HTMLElement | null>();
    const itemsRef = useRef<(HTMLAnchorElement | null)[]>([]);
    const [isDropdownOpen, setIsDropdownOpen] = useState<boolean>(false);
    const dropdownRef = useRef<HTMLDivElement | null>(null);
    const [hideMenu, setHideMenu] = useState<boolean>(false);
    const [isMenuOpened, setIsMenuOpened] = useState<boolean>(false);
    const [currentBadge, setCurrentBadge] = useState<string | null>(null);

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const checkAuthMethodLogoutAsync = async () => {
        const validAuthMethods = ["jwt", "solana"] as const;
        type AuthMethod = typeof validAuthMethods[number];

        if (!authMethod || !validAuthMethods.includes(authMethod as AuthMethod)) {
            await dispatch(logoutAsync());
            return;
        }

        if (authMethod === "jwt") {
            await dispatch(logoutAsync());
        } else if (authMethod === "solana") {
            dispatch(solanaLogoutAsync());
        }
    };

    const handleLogout = async () => {
        try {
            await checkAuthMethodLogoutAsync();
            setIsDropdownOpen(false);
            navigate('/');
        } catch (error) {
            console.error("Logout failed:", error);
        }
    };

    useEffect(() => {
        if (hideMenu === false && isMenuOpened === true)
            setIsMenuOpened(false);
    }, [hideMenu]);

    useEffect(() => {
        checkMenuWidth();
    }, [props.items]);

    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
                setIsDropdownOpen(false);
            }
        };
        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    useLayoutEffect(() => {
        if (!containerRef.current) return;

        const observer = new ResizeObserver(() => checkMenuWidth());
        observer.observe(containerRef.current);

        checkMenuWidth();

        return () => observer.disconnect();
    }, []);

    const checkMenuWidth = () => {
        const containerWidth = containerRef.current != null ? containerRef.current.offsetWidth : 0;
        const itemsContainerWidth = containerItemsRef.current != null ? containerItemsRef.current.offsetWidth : 0;

        let itemsLength = CalculateMenuItemsWidth(itemsRef.current);

        if (widthToHide.current !== null) {
            if (containerWidth >= widthToHide.current) {
                setHideMenu(false);
            } else {
                setHideMenu(true);
            }
        } else {
            if (itemsContainerWidth > 0) {
                if (itemsLength >= itemsContainerWidth) {
                    widthToHide.current = containerWidth + (itemsLength - itemsContainerWidth) + 100;
                    setHideMenu(true);
                } else {
                    if (hideMenu === true) {
                        setHideMenu(false);
                    }
                }
            }
        }
    };

    const formatUsername = (username: string): string => {
        const atIndex = username.indexOf('@');
        return atIndex !== -1 ? username.slice(0, atIndex) : username;
    };

    useEffect(() => {
        if (solanaUserProfile?.id) {
            dispatch(getAllBadgesByUserIdAsync(solanaUserProfile.id))
                .unwrap()
                .then((badges) => {
                    if (badges.length > 0) {
                        const highestBadge = badges.reduce((prev, current) => {
                            return current.badge.requiredTasksToReceiveBadge > prev.badge.requiredTasksToReceiveBadge
                                ? current
                                : prev;
                        });
                        setCurrentBadge(highestBadge.badge.icon);
                    } else {
                        setCurrentBadge(null);
                    }
                })
                .catch((error) => console.error("Failed to fetch badges:", error));
        }
    }, [solanaUserProfile, dispatch]);

    return (
        <div
            ref={(ref) => {
                containerRef.current = ref;
            }}
            className="menu-container"
        >
            <div className="menu-container-icon">
                <img src={props.icon} onClick={() => <Navigate to={"/"}/>}/>
                <p>askly</p>
            </div>

            {hideMenu === false && (
                <>
                    <nav
                        ref={(ref) => {
                            containerItemsRef.current = ref;
                        }}
                        className="menu-container-items"
                    >
                        {props.items.map((item, index) => (
                            <Link
                                key={index}
                                className="menu-item"
                                ref={(ref) => {
                                    itemsRef.current[index] = ref;
                                }}
                                to={item.path}
                            >
                                {item.name}
                            </Link>
                        ))}
                    </nav>

                    <div className='menu-authentication'>
                        {isLogin === true ? (
                            <div ref={dropdownRef} className="user-info" style={{display: 'flex', alignItems: 'center', gap: '8px'}}>
                                {authMethod ==="jwt" && userProfile && (
                                    <>
                                        <img
                                            src={`${baseUrl}/images/avatars/${userProfile.avatarName}.png`}
                                            alt="Avatar"
                                            className="user-avatar"
                                            style={{
                                                width: '38px',
                                                height: '38px',
                                                borderRadius: '50%',
                                                border: '2px solid #ffffff',
                                                boxShadow: '0 4px 6px rgba(0, 0, 0, 0.1)',
                                                transition: 'transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out',
                                                cursor: 'pointer',
                                            }}
                                            onMouseEnter={(e) => {
                                                e.currentTarget.style.transform = 'scale(1.1)';
                                                e.currentTarget.style.boxShadow = '0 6px 8px rgba(0, 0, 0, 0.15)';
                                            }}
                                            onMouseLeave={(e) => {
                                                e.currentTarget.style.transform = 'scale(1)';
                                                e.currentTarget.style.boxShadow = '0 4px 6px rgba(0, 0, 0, 0.1)';
                                            }}
                                            onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                                        />
                                        <span
                                            className="user-name"
                                            style={{
                                                whiteSpace: 'nowrap',
                                                overflow: 'hidden',
                                                textOverflow: 'ellipsis',
                                                maxWidth: '120px',
                                                fontSize: '16px',
                                                fontWeight: '500',
                                            }}
                                        >
                                            {formatUsername(userProfile.email)}
                                        </span>
                                    </>
                                )}
                                {authMethod === "solana" && solanaUserProfile && (
                                    <div className="user-info">
                                        <img
                                            src={`${baseUrl}/images/avatars/${solanaUserProfile.avatarName}.png`}
                                            alt="Avatar"
                                            className="user-avatar"
                                            style={{
                                                width: '38px',
                                                height: '38px',
                                                borderRadius: '50%',
                                                border: '2px solid #ffffff',
                                                boxShadow: '0 4px 6px rgba(0, 0, 0, 0.1)',
                                                transition: 'transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out',
                                                cursor: 'pointer',
                                            }}
                                            onMouseEnter={(e) => {
                                                e.currentTarget.style.transform = 'scale(1.1)';
                                                e.currentTarget.style.boxShadow = '0 6px 8px rgba(0, 0, 0, 0.15)';
                                            }}
                                            onMouseLeave={(e) => {
                                                e.currentTarget.style.transform = 'scale(1)';
                                                e.currentTarget.style.boxShadow = '0 4px 6px rgba(0, 0, 0, 0.1)';
                                            }}
                                            onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                                        />
                                        <span
                                            className="user-name"
                                            style={{
                                                whiteSpace: 'nowrap',
                                                overflow: 'hidden',
                                                textOverflow: 'ellipsis',
                                                maxWidth: '120px',
                                                fontSize: '16px',
                                                fontWeight: '500',
                                            }}
                                        >
            {solanaUserProfile.userName}
        </span>
                                        {currentBadge && (
                                            <div className="user-badge">
                                                <img
                                                    src={`${baseUrl}/images/badges/${currentBadge}.png`}
                                                    alt={`${currentBadge} badge`}
                                                    style={{ width: '100%', height: '100%', objectFit: 'contain' }}
                                                />
                                            </div>
                                        )}
                                    </div>
                                )}
                                {isDropdownOpen && (
                                    <div
                                        className="dropdown-menu"
                                        style={{
                                            position: 'absolute',
                                            top: '55px',
                                            background: '#ffffff',
                                            boxShadow: '0px 8px 16px rgba(0, 0, 0, 0.15)',
                                            borderRadius: '8px',
                                            zIndex: 1000,
                                            width: '200px',
                                            overflow: 'hidden',
                                            animation: 'fadeIn 0.3s ease-in-out',
                                        }}
                                    >
                                        <Link
                                            to="/edit-profile"
                                            className="dropdown-item"
                                            style={{
                                                display: 'block',
                                                padding: '12px 16px',
                                                color: '#333333',
                                                textDecoration: 'none',
                                                fontSize: '14px',
                                                transition: 'background-color 0.2s ease-in-out, color 0.2s ease-in-out',
                                            }}
                                            onMouseEnter={(e) => {
                                                e.currentTarget.style.backgroundColor = '#f0f0f0';
                                                e.currentTarget.style.color = '#007bff';
                                            }}
                                            onMouseLeave={(e) => {
                                                e.currentTarget.style.backgroundColor = 'transparent';
                                                e.currentTarget.style.color = '#333333';
                                            }}
                                        >
                                            Profile
                                        </Link>
                                        <Link
                                            to=""
                                            className="dropdown-item"
                                            style={{
                                                display: 'block',
                                                padding: '12px 16px',
                                                color: '#333333',
                                                textDecoration: 'none',
                                                fontSize: '14px',
                                                transition: 'background-color 0.2s ease-in-out, color 0.2s ease-in-out',
                                            }}
                                            onMouseEnter={(e) => {
                                                e.currentTarget.style.backgroundColor = '#ffebee';
                                                e.currentTarget.style.color = '#e53935';
                                            }}
                                            onMouseLeave={(e) => {
                                                e.currentTarget.style.backgroundColor = 'transparent';
                                                e.currentTarget.style.color = '#333333';
                                            }}
                                            onClick={(e) => {
                                                e.preventDefault();
                                                e.stopPropagation();
                                                handleLogout();
                                            }}
                                        >
                                            Logout
                                        </Link>
                                    </div>
                                )}
                            </div>
                        ) : (
                            <div className='menu-authentication-buttons'>
                                <button>
                                    <Link to="/authentication/login">Sign in</Link>
                                </button>
                                <button>
                                    <Link to="/authentication/register">Sign up</Link>
                                </button>
                            </div>
                        )}
                    </div>
                </>
            )}

            {hideMenu === true && (
                <div className='hiden-menu-icon'>
                    <img
                        src={isMenuOpened === true ? menu_icon_opened : menu_icon}
                        alt="Menu Icon"
                        onClick={() => setIsMenuOpened(!isMenuOpened)}
                    />
                </div>
            )}
            {isMenuOpened === true && <HideMenuContainer items={props.items} closeHideMenu={setIsMenuOpened}/>}
        </div>
    );
};