import React from "react";
import { useMsal } from "@azure/msal-react";
import { useDispatch } from "react-redux";
import { clearAccessToken } from "@/store/auth-slice";

// Add isMobile prop to conditionally style the button
export const LogOut = ({ isMobile }: { isMobile?: boolean }) => {
    const { instance } = useMsal();
    const dispatch = useDispatch();

    const handleLogout = async () => {
        const logoutRequest = {
            account: instance.getActiveAccount(),
        };

        // Clear the access token from Redux before triggering logout
        dispatch(clearAccessToken());

        // Perform MSAL logout (this will redirect the user)
        await instance.logout(logoutRequest);
    };

    return (
        <div>
            <button
                onClick={handleLogout}
                className={
                    isMobile
                        ? "block text-gray-800 hover:text-gray-400 px-3 py-2 rounded-md text-base font-medium"
                        : "text-gray-800 hover:text-gray-400 px-3 py-2 rounded-md text-sm font-extrabold"
                }
            >
                Log Out
            </button>
        </div>
    );
};
