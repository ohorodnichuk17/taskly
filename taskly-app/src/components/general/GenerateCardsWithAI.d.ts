import '../../styles/general/generate-cards-with-ai-style.scss';
import { GenerateCardsWithAIType } from "../../validation_types/types";
interface IGenerateCardsWithAI {
    handleSubmit: (request: GenerateCardsWithAIType) => void;
    onClose: () => void;
}
export declare const GenerateCardsWithAI: (props: IGenerateCardsWithAI) => import("react/jsx-runtime").JSX.Element;
export {};
