import '../../styles/general/menu-container-style.scss';
export interface IMenuContainer {
    icon: string;
    items: {
        name: string;
        path: string;
        sub_items: {
            name: string;
            path: string;
        }[];
    }[];
}
export declare const MenuContainer: (props: IMenuContainer) => import("react/jsx-runtime").JSX.Element;
