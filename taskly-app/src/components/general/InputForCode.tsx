import { useRef } from 'react';
import '../../styles/general/input-for-code-style.scss';
import { UseFormRegister } from 'react-hook-form';
import { EmailVerificationType } from '../../validation_types/types';


export interface IInputCode {
    register: UseFormRegister<EmailVerificationType>
    lastInput: React.Dispatch<React.SetStateAction<HTMLInputElement | null>>
}

export const InputForCode = (props: IInputCode) => {

    const focusRef = useRef<(HTMLInputElement | null)[]>(Array(7).fill(null));

    return (
        <div className="input-code">
            {focusRef.current.map((element, index) =>

                <input
                    key={`InputCodeKey${index}`}
                    {...props.register(`code.${index}`)}
                    ref={(_ref) => {
                        const { ref } = props.register(`code.${index}`);
                        focusRef.current[index] = _ref
                        ref(focusRef.current[index]);
                    }}
                    type="text"
                    maxLength={1}
                    autoFocus={index === 0}
                    onKeyDown={(e) => {
                        if (e.key === "ArrowRight" || e.key === "ArrowLeft" || (e.key.match("[0-9]") && e.key.length == 1) || e.key === "Backspace" || (e.ctrlKey && (e.key === "V" || e.key === "v"))) {
                            if (e.key === "ArrowRight") {
                                const nextIndex = (index + 1) % focusRef.current.length;
                                focusRef.current[nextIndex]?.focus();
                            }
                            else if (e.key === "ArrowLeft") {
                                const prevIndex = (index - 1 + focusRef.current.length) % focusRef.current.length;
                                focusRef.current[prevIndex]?.focus();
                            }
                            else if (e.key.match("[0-9]") && e.key.length == 1) {
                                focusRef.current[index]!.value = e.key;
                                const nextIndex = index + 1 <= focusRef.current.length - 1 ? index + 1 : focusRef.current.length - 1;
                                focusRef.current[nextIndex]?.focus();
                                if (!focusRef.current.find(ref => ref?.value === "")) props.lastInput(focusRef.current[index]!);
                                e.preventDefault();
                            }
                            else if (e.key === "Backspace" && focusRef.current[index]?.value) {
                                focusRef.current[index]!.value = "";
                                const prevIndex = index - 1 >= 0 ? index - 1 : 0;
                                focusRef.current[prevIndex]?.focus();
                                e.preventDefault();
                            }
                        }
                        else {
                            e.preventDefault(); // Скасування стандартної поведінки браузера
                        }


                    }}
                    onFocus={() => {
                        setTimeout(() => {
                            focusRef.current[index]?.setSelectionRange(focusRef.current[index].value.length || 0, focusRef.current[index].value.length || 0);
                        }, 0);

                    }}
                    onPaste={(e) => {
                        const clipboardData = e.clipboardData.getData("text");

                        if (clipboardData) {
                            const isValid = clipboardData.split("").every(el => el.match("[0-9]"));
                            if (!isValid) {
                                e.preventDefault();
                                return;
                            }

                            focusRef.current.filter(ref => ref?.value === "")
                                .forEach((element, id) => {
                                    if (clipboardData[id]) {
                                        element!.value = clipboardData[id];
                                        element?.focus();
                                    }
                                });
                            e.preventDefault();
                        }
                    }}

                />

            )}
        </div>)
}