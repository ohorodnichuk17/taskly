import '../../styles/general/hide-menu-container-style.scss';
export interface IHideMenuContainer {
    items: {
        name: string;
        path: string;
        sub_items: {
            name: string;
            path: string;
        }[];
    }[];
    closeHideMenu: React.Dispatch<React.SetStateAction<boolean>>;
}
export declare const HideMenuContainer: (props: IHideMenuContainer) => import("react/jsx-runtime").JSX.Element;
