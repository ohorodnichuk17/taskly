import { baseUrl } from "../../axios/baseUrl"
import { IAvatarForSelect } from "../../interfaces/generalInterface"
import '../../styles/general/avatar-container-style.scss';
import exit_icon from '../../../public/icon/exit_icon.png';
import { useEffect, useRef, useState } from "react";
import { useAppDispatch, useRootState } from "../../redux/hooks";
import { getAllAvatarsAsync } from "../../redux/actions/authenticateAction";

interface IAvatarContainer {
    selectAvatar: React.Dispatch<React.SetStateAction<string | null>>,
    avatarContainerIsOpened: boolean,
    close: () => void
}

export const AvatarConatiner = (props: IAvatarContainer) => {

    const avatars = useRootState(s => s.authenticate.avatars);
    const dispatch = useAppDispatch();
    const refAvatarList = useRef<HTMLDivElement | null>(null);
    //const [avatarContainerIsOpened, setAvatarContainerIsOpened] = useState<boolean>(true);

    /*useEffect(() => {
        setAvatarContainerIsOpened(props.avatarContainerIsOpened);
    }, [props])*/
    return (
        <div className="avatar-background-container" style={{ display: props.avatarContainerIsOpened ? "flex" : "none" }}>
            <div className="avatar-conteiner">
                <div className="exit">
                    <img src={exit_icon} alt="Exit button" onClick={props.close} />
                </div>
                <div
                    ref={(ref) =>
                        refAvatarList.current = ref}
                    className="avatar-list"
                    style={{ overflowY: refAvatarList.current && (refAvatarList.current.scrollHeight > refAvatarList.current.clientHeight) ? "scroll" : "auto" }}
                >
                    {avatars && avatars.map((element) => (
                        <img key={`${element.id}`} src={baseUrl + "/images/avatars/" + element.name + ".png"} onClick={(e) => {
                            e.preventDefault();
                            props.selectAvatar(element.id)
                            props.close()
                        }} />
                    ))}
                </div>
            </div>
        </div>
    )
}