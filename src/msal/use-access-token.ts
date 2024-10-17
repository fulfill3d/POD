import {useSelector} from "react-redux";
import {RootState} from "@/store"; // Import your store types

export const useAccessToken = () => {
    return useSelector((state: RootState) => state.auth.accessToken);
};
