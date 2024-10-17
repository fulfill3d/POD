import React from "react";
import MsalAuthProvider from "@/msal/auth-provider";
import { LogIn } from "@/msal/log-in";
import { LogOut } from "@/msal/log-out";

export const IdentityControl = ({ isMobile = false }: { isMobile?: boolean }) => {
    return (
        <MsalAuthProvider>
            <MsalAuthProvider.Public>
                <LogIn isMobile={isMobile} /> {/* Pass isMobile as a prop */}
            </MsalAuthProvider.Public>
            <MsalAuthProvider.Protected>
                <LogOut isMobile={isMobile} /> {/* Pass isMobile as a prop */}
            </MsalAuthProvider.Protected>
        </MsalAuthProvider>
    );
};
