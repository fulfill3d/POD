import React from 'react';
import {useBraintreeClientToken} from "@/hooks/payment/use-braintree-client-token";
import {useBraintreeSetup} from "@/hooks/payment/use-braintree-setup";
import {useAccessToken} from "@/msal/use-access-token";

interface BraintreePayPalButtonProps {
    callback: () => void
}

export const BraintreePaypalButton = ({callback}: BraintreePayPalButtonProps) => {
    const accessToken = useAccessToken();
    const clientToken = useBraintreeClientToken(accessToken);
    const isLoading = useBraintreeSetup(clientToken, callback, accessToken);

    return (
        <div style={{position: 'relative', minHeight: '50px'}}>
            {isLoading && (
                <div>Loading...</div>
            )}
            <div style={{width: "300px", height: "50px"}} id="paypal-button-container" />
        </div>
    );
};
