import '../../styles/general/avatar-container-style.scss';
interface IAvatarContainer {
    selectAvatar: React.Dispatch<React.SetStateAction<string | null>>;
    avatarContainerIsOpened: boolean;
    close: () => void;
}
export declare const AvatarConatiner: (props: IAvatarContainer) => import("react/jsx-runtime").JSX.Element;
export {};
