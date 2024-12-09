import React from "react";
import { useMsal } from "@azure/msal-react";
import { loginRequest } from "@/msal/config";

// Add isMobile prop to conditionally style the button
export const LogIn = ({ isMobile }: { isMobile?: boolean }) => {
    const { instance } = useMsal();

    const handleLogin = async () => {
        await instance.loginRedirect(loginRequest);
    };

    return (
        <div>
            <button
                onClick={handleLogin}
                className={
                    isMobile
                        ? "block text-gray-800 hover:text-gray-400 px-3 py-2 rounded-md text-base font-medium"
                        : "text-gray-800 hover:text-gray-400 px-3 py-2 rounded-md text-sm font-extrabold"
                }
            >
                Log In
            </button>
        </div>
    );
};
