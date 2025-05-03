import '../../styles/general/template-of-card-style.scss';
import { NewCardType } from '../../validation_types/types';
interface ITemplateOfCard {
    handleSubmit: (request: NewCardType) => void;
    onClose: () => void;
}
export declare const TemplateOfCard: (props: ITemplateOfCard) => import("react/jsx-runtime").JSX.Element;
export {};
