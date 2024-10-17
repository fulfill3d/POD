import {useEffect, useState} from "react";
import {useAccessToken} from "@/msal/use-access-token";
import useHttp from "@/hooks/common/use-http";
import {paymentEndpoints} from "@/utils/endpoints";

export type StripeIntentType = { clientSecret: string } | null;

export const useStripeSetupIntent = () => {
    const accessToken = useAccessToken();
    const { loading, error, request } = useHttp();
    const [stripeIntent, setStripeIntent] = useState<StripeIntentType>(null);

    useEffect(() => {
        request(
            paymentEndpoints.GetStripeSetupIntent.Uri,
            paymentEndpoints.GetStripeSetupIntent.Method,
            null,
            undefined,
            accessToken
        ).then(response => setStripeIntent(response));
    }, [accessToken, request]);

    return stripeIntent;
};
