import { useEffect, useRef, useState } from "react";
import { IInformationAlert, TypeOfInformation } from "../../interfaces/generalInterface";
import '../../styles/general/information-alert-style.scss';
import success_icon from '../../assets/icon/success_icon.png';
import error_icon from '../../assets/icon/error_icon.png';
import { useAppDispatch } from "../../redux/hooks";
import { clearInformation } from "../../redux/slices/generalSlice";
import arrow_bottom from '../../assets/icon/arrow_bottom_icon.png';

const INFORMATION_ALERT_DEFAULT_HEIGHT = 70;

const MESSAGE_DEFAULT_HEIGHT = 30;

export const InformationAlert = (props: IInformationAlert) => {
    const dispatch = useAppDispatch();
    const [informationContainerHeight, setInformationContainerHeight] = useState<number>(INFORMATION_ALERT_DEFAULT_HEIGHT);
    const [messageHight, setMessageHight] = useState<number>(MESSAGE_DEFAULT_HEIGHT);
    const [isArrow, setIsArrow] = useState<boolean>(false);
    const closeAlert = async () => {
        await new Promise(() => {
            setTimeout(() => {
                dispatch(clearInformation())
                setInformationContainerHeight(INFORMATION_ALERT_DEFAULT_HEIGHT);
            }, 5000);
        })
    }
    useEffect(() => {
        closeAlert();
    }, [])
    const messageRef = useRef<HTMLDivElement | null>(null);
    useEffect(() => {
        if (messageRef.current !== null) {
            setMessageHight(messageRef.current.scrollHeight);
        }
    }, [messageRef.current])
    useEffect(() => {
        if (messageHight > MESSAGE_DEFAULT_HEIGHT) {
            setIsArrow(true);
        }
    }, [messageHight])
    return (
        <div
            className="information-container"
            style={{
                boxShadow: (props.type == TypeOfInformation.Error ? "rgba(243, 27, 27, 0.34) 0px 8px 24px" : "rgba(45, 243, 27, 0.34) 0px 8px 24px"),
                height: `${informationContainerHeight}px`
            }}
            onMouseEnter={() => {
                setInformationContainerHeight(messageRef.current!.scrollHeight + 40);
                if (messageHight > MESSAGE_DEFAULT_HEIGHT) setIsArrow(false);
            }}
            onMouseLeave={() => {
                setInformationContainerHeight(INFORMATION_ALERT_DEFAULT_HEIGHT);
                if (messageHight > MESSAGE_DEFAULT_HEIGHT) setIsArrow(true);
            }}
        >

            <img src={props.type == TypeOfInformation.Error ? error_icon : success_icon} alt="Information icon" />
            <div
                ref={(ref) => {
                    messageRef.current = ref;

                }}
                className="information-container-message">
                {props.message}
            </div>


            {isArrow === true && <img src={arrow_bottom} />}
        </div>)
}
