import '../../styles/general/task-text-area-style.scss';
interface ITaskTextArea {
    defaultValue?: string | null;
    onBlur?: (() => void);
    value?: React.MutableRefObject<string | null>;
    maxLength: number;
    register?: {
        name: string;
        onChange: (event: React.ChangeEvent<any>) => void;
        onBlur: (event: React.FocusEvent<any>) => void;
        ref: (instance: HTMLTextAreaElement | null) => void;
    };
}
export declare const TaskTextArea: (prop: ITaskTextArea) => import("react/jsx-runtime").JSX.Element;
export {};
