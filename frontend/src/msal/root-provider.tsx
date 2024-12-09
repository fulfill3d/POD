'use client';

import { MsalProvider } from "@azure/msal-react";
import React from "react";
import {msalInstance} from "@/msal/msal";

interface MsalAuthProviderProps {
    children: React.ReactNode;
}

const MsalRootProvider: React.FC<MsalAuthProviderProps> = ({ children }) => {
    return (
        <MsalProvider instance={msalInstance}>
            {children}
        </MsalProvider>
    );
};

export default MsalRootProvider;
