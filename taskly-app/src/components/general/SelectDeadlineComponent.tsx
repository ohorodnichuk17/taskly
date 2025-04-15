import '../../styles/general/select-deadline-component-style.scss';

export const SelectDeadlineComponent = () => {
    return (
        <div id="select-deadline-background">
            <div id="select-deadline-container">
                <div className='date-selector'>
                    <input type="date" />
                </div>
                <div className="buttons">
                    <button>Save</button>
                    <button>Cancel</button>
                </div>
            </div>
        </div>)
}