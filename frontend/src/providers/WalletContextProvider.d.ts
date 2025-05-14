import { ReactNode } from "react";
import "@solana/wallet-adapter-react-ui/styles.css";
interface WalletContextProviderProps {
    children: ReactNode;
}
export declare const WalletContextProvider: ({ children }: WalletContextProviderProps) => import("react/jsx-runtime").JSX.Element;
export {};
