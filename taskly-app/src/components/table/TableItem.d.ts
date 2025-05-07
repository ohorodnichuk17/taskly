import { ITableItem } from "../../interfaces/tableInterface.ts";
import "../../styles/table/main.scss";
interface TableItemProps {
    item: ITableItem;
}
export default function TableItem({ item }: TableItemProps): import("react/jsx-runtime").JSX.Element;
export {};
