import {useEffect, useState} from "react";
import {paymentEndpoints} from "@/utils/endpoints";
import {httpRequest} from "@/service/http-request";

export const useBraintreeClientToken = (access_token: string | null) => {
    const [clientToken, setClientToken] = useState<string | null>(null);

    useEffect(() => {
        const setClientTokenFromJson = (json: any) => {
            if (json && json.clientToken) {
                setClientToken(json.clientToken);
            }
        };
        if (access_token){
            httpRequest(
                paymentEndpoints.GetBraintreeClientToken.Uri,
                paymentEndpoints.GetBraintreeClientToken.Method,
                null,
                undefined,
                access_token)
                .then( result => {
                    setClientTokenFromJson(result);
                });
        }
    }, [access_token]);

    return clientToken;
};
