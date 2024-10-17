'use client';

import React, { createContext, useEffect, ReactNode, ReactElement } from "react";
import { initializeMsal, msalInstance } from "@/msal/msal";
import { AuthenticatedTemplate, MsalProvider, UnauthenticatedTemplate } from "@azure/msal-react";

interface MsalAuthProviderProps {
    children: ReactNode;
}

interface MsalAuthContextProps {
    publicContent: ReactNode | null;
    protectedContent: ReactNode | null;
}

const MsalAuthContext = createContext<MsalAuthContextProps>({ publicContent: null, protectedContent: null });

function isValidElementWithProps(element: ReactNode): element is ReactElement<{ children: ReactNode }> {
    return React.isValidElement(element);
}

export default function MsalAuthProvider({ children }: MsalAuthProviderProps) {
    useEffect(() => {
        initializeMsal();
    }, []);

    let publicContent: ReactNode = null;
    let protectedContent: ReactNode = null;

    React.Children.forEach(children, child => {
        if (isValidElementWithProps(child)) {
            if (child.type === MsalAuthProvider.Public) {
                publicContent = child.props.children;
            } else if (child.type === MsalAuthProvider.Protected) {
                protectedContent = child.props.children;
            }
        }
    });

    return (
        <MsalProvider instance={msalInstance}>
            <AuthenticatedTemplate>
                {protectedContent}
            </AuthenticatedTemplate>
            <UnauthenticatedTemplate>
                {publicContent}
            </UnauthenticatedTemplate>
        </MsalProvider>
    );
}

MsalAuthProvider.Public = function Public({ children }: { children: ReactNode }) {
    return <>{children}</>;
};

MsalAuthProvider.Protected = function Protected({ children }: { children: ReactNode }) {
    return <>{children}</>;
};
