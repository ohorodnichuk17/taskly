import { useEffect, useLayoutEffect, useRef, useState } from 'react';
import '../../styles/general/menu-container-style.scss';
import menu_icon from '../../../public/icon/menu_icon.png';

export interface IMenuContainer {
    icon: string;
    items: {
        name: string
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

    const widthToHide = useRef<number | null>(null);
    const containerRef = useRef<HTMLDivElement | null>();
    const containerItemsRef = useRef<HTMLElement | null>();
    const itemsRef = useRef<(HTMLAnchorElement | null)[]>([]);

    const [hideMenu, setHideMenu] = useState<boolean>(false);




    useEffect(() => {
        checkMenuWidth();
    }, [props.items]);

    useLayoutEffect(() => {
        if (!containerRef.current) return;

        const observer = new ResizeObserver(() => checkMenuWidth());
        observer.observe(containerRef.current);

        checkMenuWidth();

        return () => observer.disconnect(); // Прибираємо, коли компонент розмонтовується


    }, []);


    const checkMenuWidth = () => {
        const containerWidth = containerRef.current != null ? containerRef.current.offsetWidth : 0;
        const itemsContainerWidth = containerItemsRef.current != null ? containerItemsRef.current.offsetWidth : 0;

        let itemsLength = CalculateMenuItemsWidth(itemsRef.current)


        if (widthToHide.current !== null) {
            if (containerWidth >= widthToHide.current) {
                setHideMenu(false);
            }
            else {
                setHideMenu(true);
            }
        }
        else {
            if (itemsContainerWidth > 0) {
                if (itemsLength >= itemsContainerWidth) {
                    widthToHide.current = containerWidth + (itemsLength - itemsContainerWidth) + 100;
                    setHideMenu(true);
                }
                else {
                    if (hideMenu === true) {
                        setHideMenu(false);
                    }
                }
            }
        }

    }
    return (<div
        ref={(ref) => {
            containerRef.current = ref;

        }}
        className="menu-container"

    >
        <div className="menu-container-icon">
            <img src={props.icon} />
        </div>

        {hideMenu === false && <nav
            ref={(ref) => {
                containerItemsRef.current = ref;
            }}
            className="menu-container-items"
        >
            {props.items.map((item, index) => (
                <a className="menu-item"
                    ref={(ref) => {
                        itemsRef.current[index] = ref;
                    }}
                >
                    {item.name}
                </a>
            ))}
        </nav>}

        <div className='menu-authentication-buttons'>
            <button>Sign in</button>
            <button>Sing up</button>
        </div>
        {hideMenu === true && <div className='hiden-menu-icon'>
            <img src={menu_icon} alt="Menu Icon" />
        </div>}
    </div>)
}