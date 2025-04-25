import { useEffect, useRef, useState } from "react";
import '../../styles/general/task-text-area-style.scss';
import { FieldValues, Path, UseFormRegister, UseFormRegisterReturn } from "react-hook-form";

interface ITaskTextArea {
    defaultValue?: string | null,
    onBlur?: (() => void),
    value: React.MutableRefObject<string | null>,
    maxLength: number,
    register?: {
        name: string;
        onChange: (event: React.ChangeEvent<any>) => void;
        onBlur: (event: React.FocusEvent<any>) => void;
        ref: (instance: HTMLTextAreaElement | null) => void;
    };
    //register?: UseFormRegisterReturn
}
export const TaskTextArea = (prop: ITaskTextArea) => {
    const descriptionTextAreaRef = useRef<HTMLTextAreaElement | null>(null);
    const [lengthOfText, setLengtOfText] = useState<number>(0);

    useEffect(() => {
        prop.value.current = prop.defaultValue ? prop.defaultValue : "";
        setLengtOfText(prop.defaultValue ? prop.defaultValue.length : 0);
    }, [])
    useEffect(() => {
        if (descriptionTextAreaRef.current) {
            descriptionTextAreaRef.current.focus();
            descriptionTextAreaRef.current.setSelectionRange(
                descriptionTextAreaRef.current.value.length,
                descriptionTextAreaRef.current.value.length);

        }
    }, [descriptionTextAreaRef.current])
    useEffect(() => {
        if (descriptionTextAreaRef.current) {
            descriptionTextAreaRef.current.style.height = '0px';
            descriptionTextAreaRef.current.style.height = `${descriptionTextAreaRef.current.scrollHeight}px`;
        }
    }, [descriptionTextAreaRef.current?.value])

    return (
        <>
            <textarea


                name="card_description"
                id=""
                defaultValue={prop.defaultValue ? prop.defaultValue : ""}
                maxLength={prop.maxLength}
                ref={(_ref) => {
                    descriptionTextAreaRef.current = _ref;
                    //prop.register?.ref(_ref);
                    if (prop.register) {
                        const { ref } = prop.register;
                        ref(descriptionTextAreaRef.current);
                    }
                }}
                onBlur={prop.onBlur ? prop.onBlur : () => { }}
                onChange={(e) => {
                    prop.value.current = descriptionTextAreaRef.current ? descriptionTextAreaRef.current.value : "";
                    if (descriptionTextAreaRef.current) {
                        descriptionTextAreaRef.current.style.height = '0px';
                        descriptionTextAreaRef.current.style.height = `${descriptionTextAreaRef.current.scrollHeight}px`;
                        setLengtOfText(descriptionTextAreaRef.current.value.length);
                    }
                    if (prop.register) {
                        prop.register.onChange?.(e);
                    }

                }}

            ></textarea>
            <div className="length-of-task">
                {`${lengthOfText}/${prop.maxLength}`}
            </div>
        </>

    )
}
/*

*/