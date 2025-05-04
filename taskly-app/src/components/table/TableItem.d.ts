import { ITableItem } from "../../interfaces/tableInterface.ts";
import "../../styles/table/main.scss";
<<<<<<< HEAD
export default function TableItem({ item }: ITableItem): import("react/jsx-runtime").JSX.Element;
=======
interface TableItemProps {
    item: ITableItem;
}
export default function TableItem({ item }: TableItemProps): import("react/jsx-runtime").JSX.Element;
export {};
>>>>>>> d03efc386315301a4c81be8b9cc25da9c7260788
