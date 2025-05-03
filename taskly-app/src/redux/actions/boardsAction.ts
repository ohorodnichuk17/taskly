import { createAsyncThunk } from "@reduxjs/toolkit";
import { IAddMemberRequest, ICardListItem, ICreateBoard, IMemberOfBoard, IRemoveMemberFromBoard, ITemplateOfBoard, IUsersBoard } from "../../interfaces/boardInterface";
import { IValidationErrors } from "../../interfaces/generalInterface";
import { api } from "../../axios/api";
import { AxiosError } from "axios";

export const getBoardsByUserAsync = createAsyncThunk<
    IUsersBoard[],
    void,
    { rejectValue: IValidationErrors }>(
        "board/get-boards-by-user",
        async (_, { rejectWithValue }) => {
            try {
                var response = await api.get("/api/board/get-boards-by-user",
                    { withCredentials: true }
                );

                return response.data.$values;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }


        }
    )

export const getCardsListsByBoardIdAsync = createAsyncThunk<
    ICardListItem[],
    string,
    { rejectValue: IValidationErrors }>(
        "board/get-card-list-by-board-id",
        async (boardId: string, { rejectWithValue }) => {
            try {
                const response = await api.get(`/api/Cards/get-card-list-by-board-id?boardId=${boardId}`,
                    {
                        withCredentials: true
                    }
                );
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }
        }
    )

export const leaveBoardAsync = createAsyncThunk<
    string[],
    string,
    { rejectValue: IValidationErrors }>(
        "board/leave-board",
        async (boardId: string, { rejectWithValue }) => {
            try {
                const response = await api.put(`/api/board/leave-board`, {
                    boardId: boardId
                },
                    {
                        withCredentials: true
                    }
                );
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }
        }
    )

export const addMemberToBoardAsync = createAsyncThunk<
    IMemberOfBoard,
    IAddMemberRequest,
    { rejectValue: IValidationErrors }>(
        "board/add-member",
        async (request: IAddMemberRequest, { rejectWithValue }) => {
            try {
                const response = await api.post(`/api/board/add-member`, {
                    boardId: request.boardId,
                    memberEmail: request.memberEmail
                },
                    {
                        withCredentials: true
                    }
                );
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }
        }
    )

export const getMembersOfBoardAsync = createAsyncThunk<
    IMemberOfBoard[],
    string,
    { rejectValue: IValidationErrors }>(
        "board/get-members-of-board",
        async (boardId: string, { rejectWithValue }) => {
            try {
                const response = await api.get(`/api/board/members/${boardId}`,
                    {
                        withCredentials: true
                    }
                );
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }
        }
    )

export const removeMemberFromBoardAsync = createAsyncThunk<
    string[],
    IRemoveMemberFromBoard,
    { rejectValue: IValidationErrors }>(
        "board/remove-member-from-board",
        async (request: IRemoveMemberFromBoard, { rejectWithValue }) => {
            try {
                const response = await api.delete(`/api/board/remove-member`,
                    {
                        data: {
                            boardId: request.boardId,
                            userId: request.userId
                        },
                        withCredentials: true
                    }
                );
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }
        }
    )

export const getTemplatesOfBoardAsync = createAsyncThunk<
    ITemplateOfBoard[],
    void,
    { rejectValue: IValidationErrors }>(
        "board/get-templates-of-board",
        async (_, { rejectWithValue }) => {
            try {
                const response = await api.get(`/api/board/get-templates-of-board`,
                    {
                        withCredentials: true
                    }
                );
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }
        }
    )

export const createBoardAsync = createAsyncThunk<
    string,
    ICreateBoard,
    { rejectValue: IValidationErrors }>(
        "board/create-board",
        async (request: ICreateBoard, { rejectWithValue }) => {
            try {
                const response = await api.post(`/api/board/create`,
                    {
                        userId: request.userId,
                        name: request.name,
                        tag: request.tag,
                        isTeamBoard: request.isTeamBoard,
                        boardTemplateId: request.boadrTemplateId
                    },
                    {
                        withCredentials: true
                    }
                );
                return response.data;
            } catch (err: any) {
                let error: AxiosError<IValidationErrors> = err;
                if (!error.response)
                    throw err;

                return rejectWithValue(error.response.data);
            }
        }
    )