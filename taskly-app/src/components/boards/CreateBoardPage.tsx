import { useEffect, useLayoutEffect, useRef, useState } from 'react';
import '../../styles/board/create-board-container-style.scss';
import { useAppDispatch, useRootState } from '../../redux/hooks';
import { createBoardAsync, getTemplatesOfBoardAsync } from '../../redux/actions/boardsAction';
import { baseUrl } from '../../axios/baseUrl';
import { GeneralMode } from '../general/GeneralModal';
import { useForm } from 'react-hook-form';
import { CreateBoardType, CreateBoardShema } from '../../validation_types/types';
import { zodResolver } from '@hookform/resolvers/zod';
import { InputMessage, typeOfMessage } from '../general/InputMessage';
import { useNavigate } from 'react-router-dom';

export const CreateBoardPage = () => {

    const user = useRootState((s) => s.authenticate.userProfile)
    const templatesOfBoard = useRootState((s) => s.board.templatesOfBoard);

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const [selectedTemplate, setTemplate] = useState<{
        id: string,
        name: string,
        imagePath: string
    } | null>(null);
    const [isOpened, setIsOpened] = useState<boolean>(false);
    const [templateOverflowY, setTemplateOverflowY] = useState<"auto" | "scroll">("auto");

    const {
        register,
        handleSubmit,
        formState: {
            errors
        },
        setValue
    } = useForm<CreateBoardType>({
        resolver: zodResolver(CreateBoardShema)
    });

    const templateRef = useRef<HTMLDivElement | null>(null);

    const getTemplates = async () => {
        await dispatch(getTemplatesOfBoardAsync());
    }
    const createBoard = async (request: CreateBoardType) => {
        const response = await dispatch(createBoardAsync({
            userId: user!.id,
            name: request.name,
            tag: request.tag === '' || request.tag === null ? null : request.tag,
            isTeamBoard: false,
            boadrTemplateId: request.boadrTemplateId
        }));

        if (createBoardAsync.fulfilled.match(response)) {
            navigate("/boards")
        }
    }

    useLayoutEffect(() => {
        getTemplates();
    }, [])
    useEffect(() => {
        if (templatesOfBoard) {
            setTemplate({
                id: templatesOfBoard[0].id,
                name: templatesOfBoard[0].name,
                imagePath: templatesOfBoard[0].imagePath
            });
            setValue("boadrTemplateId", templatesOfBoard[0].id);
        }
    }, [templatesOfBoard])
    useEffect(() => {
        if (templateRef.current && templateRef.current.offsetHeight < templateRef.current.scrollHeight) {
            setTemplateOverflowY("scroll");
        }
    }, [templateRef.current])

    return (<div className="create-board-container">
        <GeneralMode
            isOpened={isOpened}
            onClose={() => setIsOpened(false)}
            children={
                <div className='templates-of-board'
                    ref={(ref) => {
                        templateRef.current = ref;
                    }}
                    style={{ overflowY: templateOverflowY }}
                >
                    {templatesOfBoard && templatesOfBoard.map((template) => (
                        <div className='template-of-board'
                            onClick={() => {
                                setTemplate({
                                    id: template.id,
                                    name: template.name,
                                    imagePath: template.imagePath
                                });
                                setValue("boadrTemplateId", template.id);
                            }}
                            style={{ border: selectedTemplate && selectedTemplate.id == template.id ? "2px solid #6822ca" : "none" }}
                        >
                            <img src={`${baseUrl}/images/dashboard_templates/${template.imagePath}.jpg`} alt="" />
                        </div>
                    ))}
                </div>
            }
            selectedItem={setTemplate}

        ></GeneralMode>
        <form onSubmit={handleSubmit(createBoard)}>
            <input {...register("name")} type="text" placeholder='Board name' />
            {errors.name && errors.name.message && <InputMessage message={errors.name.message} typeOfMessage={typeOfMessage.error} />}
            <input {...register("tag")} type="text" placeholder='Board tag' />
            {errors.tag && errors.tag.message && <InputMessage message={errors.tag.message} typeOfMessage={typeOfMessage.error} />}
            <div className='select-template'>
                {selectedTemplate &&
                    <>
                        <img src={`${baseUrl}/images/dashboard_templates/${selectedTemplate.imagePath}.jpg`} alt="" />
                        <div className='select-template-additional-information'>
                            <button
                                onClick={(e) => {
                                    e.preventDefault();
                                    setIsOpened(true)
                                }}
                            >Select</button>
                            <div className='name-of-template'>
                                {selectedTemplate.imagePath}
                            </div>
                        </div>
                    </>
                }
            </div>
            <div className='buttons'>
                <button type='submit'>Create</button>
                <button>Cancel</button>
            </div>
        </form>
    </div>)
}