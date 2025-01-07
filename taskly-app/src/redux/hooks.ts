import { useDispatch, useSelector } from "react-redux";
import type { RootState, AppDispatch } from "../redux/store.ts";

export const useRootState = useSelector.withTypes<RootState>();
export const useAppDispatch = useDispatch.withTypes<AppDispatch>();