import type { Metadata } from "next";
import "./globals.css";
import React from "react";
import ReduxProvider from "@/components/common/redux-provider";
import MsalRootProvider from "@/msal/root-provider";
import NavBar from "@/components/common/nav-bar";
import Fulfill3d from "@/svg/fulfill3d";

export const metadata: Metadata = {
  title: "POD",
  description: "pod.fulfill3d.com",
};

interface RootLayoutProps {
    children: React.ReactNode
}

const RootLayout: React.FC<Readonly<RootLayoutProps>> = ({ children }) => {
    return (
        <html lang="en">
        <body className="h-screen flex flex-col">
        <ReduxProvider>
            <MsalRootProvider>
                <NavBar brandName="POD" logoSvg={Fulfill3d} links={[]}/>
                <main className="flex-1 pt-16 overflow-hidden">
                    {children}
                </main>
            </MsalRootProvider>
        </ReduxProvider>
        </body>
        </html>
    );
}

export default RootLayout;
