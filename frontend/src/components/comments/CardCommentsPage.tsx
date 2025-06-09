import { useEffect, useRef, useState } from "react";
import { useAppDispatch, useRootState } from "../../redux/hooks"
import { getCommentsByCardIdAsync } from "../../redux/actions/commentsActions";
import { CardCommentComponent } from "./CardCommentComponent";
import "../../styles/cardComments/card-comments-page-style.scss";
import { ICardComment } from "../../interfaces/cardCommentsInterface";
import { ISolanaUserProfile, IUserProfile } from "../../interfaces/authenticateInterfaces";
import { LeaveCommentComponent } from "./LeaveCommentComponent";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { baseUrl } from "../../axios/baseUrl";
import { LeaveCommentType } from "../../validation_types/types";

interface ICardCommentsPage {
    cardId: string
}
export const CardCommentsPage = (props: ICardCommentsPage) => {


    const conn = useRef<HubConnection | null>(null);
    const cardCommentsPageRef = useRef<HTMLDivElement | null>();
    const cardCommentsListRef = useRef<HTMLDivElement | null>();
    const user = useRef<IUserProfile | ISolanaUserProfile | null>(null);

    const [cardCommentsListOverflowY, setCardCommentsListOverflowY] = useState<"auto" | "scroll">("auto");
    const [cardCommentsList, setCardCommentsList] = useState<ICardComment[] | null>(null);

    const authMethod = useRootState((s) => s.authenticate.authMethod);
    const cardComments = useRootState(s => s.cardComments.cardComments);
    const userJwt = useRootState((s) => s.authenticate.userProfile);
    const userSolana = useRootState((s) => s.authenticate.solanaUserProfile);



    const dispatch = useAppDispatch();

    const getCardCommentsAsync = async () => {
        await dispatch(getCommentsByCardIdAsync(props.cardId));
    }
    const startConnection = async () => {
        conn.current = new HubConnectionBuilder()
            .withUrl(`${baseUrl}/card-comments`)
            .configureLogging(LogLevel.Information)
            .build();

        conn.current.on("ConnectToCardComments", (msg) => {
            console.log(msg)
        })
        conn.current.on("DisconnectFromCardComments", (msg) => {
            console.log(msg)
        })
        conn.current.on("LeaveComment", (obj: ICardComment) => {
            console.log("NEW COMMENT - ", obj);
            addCommentToList(obj);
        })

        await conn.current.start();
        await conn.current.invoke("ConnectToCardComments", {
            cardId: props.cardId,
            userId: user.current?.id
        });
    }
    const endConnection = async () => {
        if (conn.current) {
            await conn.current.invoke("DisconnectFromCardComments", {
                cardId: props.cardId,
                userId: user.current?.id
            });
            await conn.current.stop();
        }

    }


    useEffect(() => {
        if (props.cardId != null)
            getCardCommentsAsync();
    }, [props.cardId])
    useEffect(() => {
        setCardCommentsList(cardComments);
        console.log("card comments - ", cardComments);
        if (cardCommentsListRef.current && cardCommentsListRef.current.scrollHeight > cardCommentsListRef.current.offsetHeight) {
            setCardCommentsListOverflowY("scroll")
        }
        else {
            setCardCommentsListOverflowY("auto")
        }
    }, [cardComments])

    useEffect(() => {
        if (authMethod) {
            if (authMethod === "jwt")
                user.current = userJwt;
            else
                user.current = userSolana;
        }
    }, [authMethod])

    useEffect(() => {
        if (!cardCommentsListRef.current) return;

        const observer = new ResizeObserver(() => {
            if (cardCommentsListRef.current && cardCommentsListRef.current.scrollHeight > cardCommentsListRef.current.offsetHeight) {
                setCardCommentsListOverflowY("scroll")
            }
            else {
                setCardCommentsListOverflowY("auto")
            }
        })
        observer.observe(cardCommentsListRef.current);

        return () => {
            observer.disconnect();
        }
    }, [])

    useEffect(() => {
        startConnection();
    }, [])
    useEffect(() => {
        return () => {
            endConnection();
        }
    }, [])
    const leaveComment = async (obj: LeaveCommentType) => {
        console.log("comm - ", obj);
        if (conn.current !== null && user.current) {
            console.log("Sending...")
            console.log("Sending data", {
                cardId: props.cardId,
                userId: user.current.id,
                text: obj.comment
            })
            await conn.current.invoke("LeaveComment", {
                cardId: props.cardId,
                userId: user.current.id,
                text: obj.comment
            })
        }
    }
    const addCommentToList = (comment: ICardComment) => {
        console.log("cardCommentsList - ", cardCommentsList);
        if (cardCommentsList) {
            const newCardList = [...cardCommentsList, {
                id: comment.id,
                text: comment.text,
                userId: comment.userId,
                userAvatar: comment.userAvatar,
                userName: comment.userName,
                createdAt: comment.createdAt
            }];
            console.log("New card list - ", newCardList);
            setCardCommentsList(newCardList);
        }
    }

    return (<div className="card-comments-page"

        ref={(ref) => {
            cardCommentsPageRef.current = ref
        }}
    >
        <div className="card-comments-list"
            style={{ overflowY: cardCommentsListOverflowY }}
            ref={(ref) => {
                cardCommentsListRef.current = ref
            }}
        >

            {cardCommentsList ? cardCommentsList.map((cardComment) => (
                user.current && user.current.id === cardComment.userId ?
                    <CardCommentComponent
                        text={cardComment.text}
                        createdAt={cardComment.createdAt}
                        isYourComment={true}
                    />
                    :
                    <CardCommentComponent
                        userId={cardComment.userId}
                        userName={cardComment.userName}
                        userAvatar={cardComment.userAvatar}
                        text={cardComment.text}
                        createdAt={cardComment.createdAt}
                        isYourComment={false}
                    />
            )) :
                <div className="empty-comments">There is not any comments.</div>
            }
        </div>

        <LeaveCommentComponent
            leaveComment={leaveComment}
        />
    </div>)
}