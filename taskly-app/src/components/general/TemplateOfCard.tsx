import { useEffect, useRef, useState } from 'react';
import '../../styles/general/template-of-card-style.scss';
import { TaskTextArea } from './TaskTextArea';
import { format } from 'date-fns';
import { Controller, useForm } from 'react-hook-form';
import { NewCardShema, NewCardType } from '../../validation_types/types';
import { zodResolver } from '@hookform/resolvers/zod';
import { useRootState } from '../../redux/hooks';
import { InputMessage, typeOfMessage } from './InputMessage';


interface ITemplateOfCard {
    handleSubmit: (request: NewCardType) => void,
    onClose: () => void
}

export const TemplateOfCard = (props: ITemplateOfCard) => {
    const userId = useRootState((s) => s.authenticate.userProfile?.id);

    const {
        register,
        handleSubmit,
        setError,
        setValue,
        formState: {
            errors
        },
        control
    } = useForm<NewCardType>({
        resolver: zodResolver(NewCardShema),
        defaultValues: {
            deadline: (() => {
                const currentDay = new Date();
                currentDay.setDate(currentDay.getDate() + 10)
                return currentDay;
            })(),
            isPublicCard: false
        }
    })

    useEffect(() => {
        console.log(errors)
    }, [errors])
    useEffect(() => {
        console.log(userId)
    }, [userId])

    return (
        <div className="template-of-card">
            <form onSubmit={handleSubmit(props.handleSubmit)}>
                <Controller
                    name="task"
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

                {errors.task && <InputMessage typeOfMessage={typeOfMessage.error} message={errors.task.message!} />}
                <div className="template-of-card-deadline">
                    <p>Deadline : </p>
                    <input
                        {...register("deadline", {
                            setValueAs: (value: string) => new Date(value)
                        })}
                        type="date"
                        min={format(Date.now(), "yyyy-MM-dd")}
                        max={format((() => {
                            const currentYear = new Date();
                            currentYear.setFullYear(currentYear.getFullYear() + 1);
                            return currentYear;
                        })(), "yyyy-MM-dd")}
                        defaultValue={format((() => {
                            const currentDay = new Date();
                            currentDay.setDate(currentDay.getDate() + 10)
                            return currentDay;
                        })(), "yyyy-MM-dd")}
                    />
                </div>
                {errors.deadline && <InputMessage typeOfMessage={typeOfMessage.error} message={errors.deadline.message!} />}
                <div className="template-of-card-type-of-task" >
                    <label>
                        <p>public task</p>
                        <input onChange={(e) => {
                            if (e.currentTarget.checked === true) {
                                setValue("isPublicCard", true);
                            }
                        }} type="radio" name="type-of-task" value={"true"} />
                    </label>
                    <label>
                        <p>personal task</p>
                        <input onChange={(e) => {
                            if (e.currentTarget.checked === true) {
                                setValue("isPublicCard", false);
                            }
                        }} type="radio" name="type-of-task" value={"false"} defaultChecked />
                    </label>
                </div>
                <div className="template-of-card-buttons">
                    <button type='submit'>Create</button>
                    <button onClick={props.onClose}>Cancel</button>
                </div>
            </form>

        </div>)
}
