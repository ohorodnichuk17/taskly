import { useEffect, useLayoutEffect, useRef } from "react"
import { useAppDispatch, useRootState } from "../../redux/hooks"
import { getBoardsByUserAsync } from "../../redux/actions/boardsAction";
import "../../styles/board/boards-page-style.scss";
import { Link } from "react-router-dom";
import { baseUrl } from "../../axios/baseUrl";
import person_icon from '../../../public/icon/person_icon.png';

export const BoardsPage = () => {

    const boards = useRootState(s => s.board.listOfBoards);
    const dispatch = useAppDispatch();

    const worcspaceContainerRef = useRef<HTMLDivElement | null>(null);

    const getBoardsByUser = async () => {
        await dispatch(getBoardsByUserAsync());
    }
    useLayoutEffect(() => {
        getBoardsByUser();
    }, [])

    /*useEffect(() => {
        
    }, [])*/
    useEffect(() => {
        console.log("boards ---> ", boards)
    }, [boards])

    return (<div className="boards-container">
        <h2>YOUR WORKSPACE</h2>
        <div className="workspace-container"
            ref={(ref) => {
                worcspaceContainerRef.current = ref;
            }}
        >
            {boards &&
                boards.map((element) => (
                    <Link to={`${element.id}`} key={element.id}>
                        <img
                            src={baseUrl + `/images/dashboard_templates/${element.boardTemplateColor}.jpg`}
                            alt={`Board template ${element.boardTemplateName}`} />
                        <p>{element.name}</p>

                        <div className="additional-information">
                            <span>2</span>
                            <img src={person_icon} alt={`Count of members ${element.countOfMemebers}`} />
                        </div>
                    </Link>
                ))
            }
            <Link to="create-new-board" id="create-new-board">
                <p>Create new board</p>
            </Link>
            <Link to="create-new-board" id="create-new-board">
                <p>Create new board</p>
            </Link>
            <Link to="create-new-board" id="create-new-board">
                <p>Create new board</p>
            </Link>
            <Link to="create-new-board" id="create-new-board">
                <p>Create new board</p>
            </Link>
            <Link to="create-new-board" id="create-new-board">
                <p>Create new board</p>
            </Link>
            <Link to="create-new-board" id="create-new-board">
                <p>Create new board</p>
            </Link>
        </div>
    </div>)
}