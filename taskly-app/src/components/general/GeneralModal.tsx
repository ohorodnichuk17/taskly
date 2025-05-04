import '../../styles/general/general-modal-style.scss';
import exit_icon from '../../assets/icon/exit_icon.png';

interface IGeneralModal<T> {
    children: React.ReactNode,
    isOpened: boolean,
    selectedItem: T | null,
    onClose: () => void,

}

export const GeneralMode = <T,>(props: IGeneralModal<T>) => {
    return (
        <div className="general-modal-background" style={{ display: props.isOpened == true ? "flex" : "none" }}>
            <div className='general-modal-container'>
                <div className="exit">
                    <img src={exit_icon} alt="Exit button"
                        onClick={() => {
                            props.onClose();
                        }} />
                </div>
                {props.children}
            </div>

        </div>
    )
}

/*export const GeneralModal = (props : IGeneralModal<T>) => {
    
}*/