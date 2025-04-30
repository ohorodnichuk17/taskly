import { useEffect, useLayoutEffect, useRef, useState } from "react"
import { useAppDispatch, useRootState } from "../../redux/hooks"
import { getBoardsByUserAsync } from "../../redux/actions/boardsAction";
import "../../styles/board/boards-page-style.scss";
import { Link } from "react-router-dom";
import { baseUrl } from "../../axios/baseUrl";
import person_icon from '../../../public/icon/person_icon.png';
import { IUsersBoard } from "../../interfaces/boardInterface";
import { GeneralMode } from "../general/GeneralModal";

function TakeCetrainCount<T>(array: Array<T>, count: number) {
    let newArray: Array<T> = [];

    array.forEach((element) => {
        if (newArray.length > count)
            return;
        newArray.push(element);
    });

    return newArray;
}
export const BoardsPage = () => {

    const boards = useRootState(s => s.board.listOfBoards);
    const dispatch = useAppDispatch();

    const worcspaceContainerRef = useRef<HTMLDivElement | null>(null);
    const boardsItemsRef = useRef<HTMLDivElement | null>(null);

    const [isOpened, setIsOpened] = useState<boolean>(false);
    const [worcspaceOverflowY, setWorcspaceOverflowY] = useState<"auto" | "scroll">("auto")
    const [boardsItemsOverflowY, setBoardsItemsOverflowY] = useState<"auto" | "scroll">("auto")

    const getBoardsByUser = async () => {
        await dispatch(getBoardsByUserAsync());
    }
    useLayoutEffect(() => {
        getBoardsByUser();
    }, [])

    useEffect(() => {
        console.log(boards);
    }, [boards])
    useEffect(() => {
        if (boardsItemsRef.current) {
            setBoardsItemsOverflowY(boardsItemsRef.current && boardsItemsRef.current.offsetHeight < boardsItemsRef.current.scrollHeight ?
                "scroll" :
                "auto")
        }

    }, [boardsItemsRef.current])

    useEffect(() => {
        if (worcspaceContainerRef.current) {
            setWorcspaceOverflowY(worcspaceContainerRef.current && worcspaceContainerRef.current.offsetHeight < worcspaceContainerRef.current.scrollHeight ?
                "scroll" :
                "auto")
        }
    }, [worcspaceContainerRef.current])

    return (<div className="boards-container">
        <GeneralMode isOpened={isOpened} selectedItem={null} onClose={() => {
            setIsOpened(false);
        }}>
            <div className="boards-items"
                ref={(ref) => {
                    boardsItemsRef.current = ref;
                }}
                style={{
                    overflowY: boardsItemsOverflowY
                }}
            >
                {boards && boards.map((element) => (
                    <Link to={`${element.id}`} key={element.id}>
                        <img
                            src={baseUrl + `/images/dashboard_templates/${element.boardTemplateColor}.jpg`}
                            alt={`Board template ${element.boardTemplateName}`} />
                        <p>{element.name}</p>

                        <div className="additional-information">
                            <span>{element.countOfMemebers}</span>
                            <img src={person_icon} alt={`Count of members ${element.countOfMemebers}`} />
                        </div>
                    </Link>))}
            </div>
        </GeneralMode>
        <h2 className="gradient-text">YOUR WORKSPACE</h2>
        <div className="workspace-container"
            ref={(ref) => {
                worcspaceContainerRef.current = ref;
            }}
            style={{
                overflowY: worcspaceOverflowY
            }}
        >
            {boards &&
                (boards.length < 8 ? boards.map((element) => (
                    <Link to={`${element.id}`} key={element.id}>
                        <img
                            src={baseUrl + `/images/dashboard_templates/${element.boardTemplateColor}.jpg`}
                            alt={`Board template ${element.boardTemplateName}`} />
                        <p>{element.name}</p>

                        <div className="additional-information">
                            <span>{element.countOfMemebers}</span>
                            <img src={person_icon} alt={`Count of members ${element.countOfMemebers}`} />
                        </div>
                    </Link>
                )) : (<>{TakeCetrainCount<IUsersBoard>(boards, 7).map((element) => (
                    <Link to={`${element.id}`} key={element.id}>
                        <img
                            src={baseUrl + `/images/dashboard_templates/${element.boardTemplateColor}.jpg`}
                            alt={`Board template ${element.boardTemplateName}`} />
                        <p>{element.name}</p>

                        <div className="additional-information">
                            <span>{element.countOfMemebers}</span>
                            <img src={person_icon} alt={`Count of members ${element.countOfMemebers}`} />
                        </div>
                    </Link>
                ))}
                    <div onClick={() => setIsOpened(true)} id="more-boards">
                        <p>More boards</p>
                    </div>
                </>))
            }
            <Link to="create-new-board" id="create-new-board">
                <p>Create new board</p>
            </Link>
        </div>

    </div>)
}