import '../../styles/general/generate-cards-with-ai-style.scss';
import { Controller, useForm } from "react-hook-form"
import { TaskTextArea } from "./TaskTextArea"
import { GenerateCardsWithAIShema, GenerateCardsWithAIType } from "../../validation_types/types"
import { zodResolver } from "@hookform/resolvers/zod"
import { InputMessage, typeOfMessage } from './InputMessage';
import { Loading } from './Loading';

interface IGenerateCardsWithAI {
    handleSubmit: (request: GenerateCardsWithAIType) => void;
    onClose: () => void;
}
export const GenerateCardsWithAI = (props: IGenerateCardsWithAI) => {

    const {
        handleSubmit,
        control,
        formState: {
            errors,
            isSubmitting
        }
    } = useForm<GenerateCardsWithAIType>({
        resolver: zodResolver(GenerateCardsWithAIShema)
    })

    return (<div className="generate-cards-with-ai">
        {isSubmitting && <Loading />}
        <form onSubmit={handleSubmit(props.handleSubmit)}>
            <Controller
                name="description"
                control={control}
                render={({ field }) => (
                    <TaskTextArea
                        maxLength={300}
                        register={{
                            name: field.name,
                            onChange: field.onChange,
                            onBlur: field.onBlur,
                            ref: field.ref,
                        }}
                    />
                )}
            />
            {errors.description && <InputMessage typeOfMessage={typeOfMessage.error} message={errors.description.message!} />}
            <div className="generate-cards-with-ai-buttons">
                <button type='submit'>Generate</button>
                <button onClick={props.onClose}>Cancel</button>
            </div>
        </form>

    </div>)
}