import { AuthenticationResult, EventType, PublicClientApplication } from "@azure/msal-browser";
import {msalConfig} from "@/msal/config";

export const msalInstance = new PublicClientApplication(msalConfig);

export function initializeMsal() {
    const accounts = msalInstance.getAllAccounts();
    if (accounts.length > 0) {
        msalInstance.setActiveAccount(accounts[0]);
    }

    msalInstance.addEventCallback(async (event) => {
        if (event.eventType === EventType.LOGIN_SUCCESS && event.payload) {
            const payload = event.payload as AuthenticationResult;
            const account = payload.account;
            msalInstance.setActiveAccount(account);
        }
    });
}
