import '../../styles/general/general-modal-style.scss';
interface IGeneralModal<T> {
    children: React.ReactNode;
    isOpened: boolean;
    selectedItem: T | null;
    onClose: () => void;
}
export declare const GeneralMode: <T>(props: IGeneralModal<T>) => import("react/jsx-runtime").JSX.Element;
export {};
