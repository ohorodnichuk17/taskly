import { useEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom"
import '../../styles/board/board-page-style.scss';
import { useAppDispatch, useRootState } from "../../redux/hooks";
import { getCardsListsByBoardIdAsync } from "../../redux/actions/boardsAction";
import { format } from "date-fns";
import setting_icon from '../../../public/icon/setting_icon.png';
import { ICard, ICardListItem } from "../../interfaces/boardInterface";
import { array } from "zod";



const addItemToArrayFromAnotherArray = (item: any, array: any[]) => {
    console.log("addItemToArrayFromAnotherArray");
    return [...array, item];
}
const findAndRemoveItemFromArray = (condition: (c: any) => boolean, array: any[]) => {
    let item = array.find(condition);
    if (item) {
        let index = array.indexOf(item);
        if (index !== -1) {
            return [...array.slice(0, index), ...array.slice(index + 1)];
        }
    }
    return array;
}

export const BoardPage = () => {
    const { boardId } = useParams();

    const cardList = useRootState(s => s.board.cardList);
    const dispatch = useAppDispatch();


    const boardPageRef = useRef<HTMLDivElement | null>(null);
    const cardsRef = useRef<{
        element: (HTMLDivElement | null)
        id: string
    }[]>([]);

    const [boardPageOverflowX, setBoardPageOverflowX] = useState<"auto" | "scroll">("auto");
    const [cardsOverflowY, setCardsOverflowY] = useState<{
        id: string,
        scroll: "auto" | "scroll"
    }[]>([]);
    const [dragenCard, setDragenCard] = useState<{
        cardId: string,
        cardListId: string
    } | null>(null);
    let [cardLists, setCardLists] = useState<ICardListItem[] | null>(null);




    const getCardList = async () => {
        if (boardId != null)
            await dispatch(getCardsListsByBoardIdAsync(boardId))
    }
    useEffect(() => {
        getCardList();
    }, [])
    useEffect(() => {
        if (cardList)
            setCardLists(cardList);
    }, [cardList])
    useEffect(() => {
        if (boardPageRef.current) {
            setBoardPageOverflowX(boardPageRef.current.offsetWidth < boardPageRef.current.scrollWidth ?
                "scroll" :
                "auto"

            )
        }
    }, [boardPageRef.current])

    useEffect(() => {
        if (cardsRef.current) {
            cardsRef.current.forEach((element) => {
                if (element.element && element.element.clientHeight > element.element.offsetHeight) {
                    const scrollableCard = cardsOverflowY.find((card) => card.id === element.id);
                    if (scrollableCard) {
                        scrollableCard.scroll = "scroll";
                    }
                }
            })

        }
    }, [cardsRef.current])

    return <div className="board-page-container"
        ref={(ref) => {
            boardPageRef.current = ref;
        }}
        style={{ overflowX: boardPageOverflowX }}
    >
        <div className="card-list-container">
            {cardLists && cardLists.map((element) => (
                <div className="card-list-item" key={element.id}>
                    <h2>{element.title}</h2>
                    <div className="card-list-cards"
                        ref={(ref) => {
                            cardsRef.current.push({
                                element: ref,
                                id: element.id
                            });

                            cardsOverflowY.push({
                                id: element.id,
                                scroll: "auto"
                            })
                        }}
                        style={{
                            overflowY:
                                cardsOverflowY.find((card) => card.id === element.id) ?
                                    cardsOverflowY.find((card) => card.id === element.id)?.scroll :
                                    "auto"
                        }}
                        onDragEnd={(e) => {
                            if (dragenCard) {

                                let dragenCardItem: ICard | null = null;
                                cardLists.forEach(item => {
                                    if (item.cards && item.id === dragenCard.cardListId) {
                                        dragenCardItem = item.cards.find(card => card.id === dragenCard.cardId) || null;
                                    }
                                })
                                console.log("dragenCardItem - ", dragenCardItem)
                                setCardLists(prev => prev ? prev.map(item => ({
                                    ...item,
                                    cards: item.cards && element.id === dragenCard.cardListId ?
                                        findAndRemoveItemFromArray(el => el.id === dragenCard.cardId, item.cards) :
                                        (item.cards && item.id === element.id && dragenCardItem ?
                                            addItemToArrayFromAnotherArray(dragenCardItem, item.cards) :
                                            item.cards
                                        )

                                })) : []);
                                console.log(dragenCardItem);

                                /*setCardLists(prev => prev ? prev.map(item => ({
                                    ...item,
                                    cards: item.cards && item.id === element.id && dragenCardItem ?
                                        addItemToArrayFromAnotherArray(dragenCardItem, item.cards) :
                                        item.cards
                                })) : []);*/
                                setDragenCard(null);
                            }

                        }}
                    >
                        {element.cards && element.cards.map((element_card) => (
                            <div className="card" key={element_card.id} onDragStart={() => {
                                setDragenCard({
                                    cardId: element_card.id,
                                    cardListId: element.id
                                });
                            }} draggable>
                                <p>{element_card.description}</p>
                                <div>
                                    <div className="card-deadline"
                                        style={{
                                            backgroundColor: (Date.parse(element_card.endTime.toString()) < Date.now()) ? "red" : "green"
                                        }}
                                    >
                                        Deadline : {format(element_card.endTime, "yyyy-MM-dd")}
                                    </div>
                                    <button>
                                        <img src={setting_icon} alt="Settings" />
                                    </button>
                                </div>

                            </div>
                        ))}
                    </div>

                </div>
            ))}
        </div>
    </div >
}