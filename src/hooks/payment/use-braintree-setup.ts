import { useEffect, useState } from "react";
import braintreeWeb from "braintree-web";
import { paymentEndpoints } from "@/utils/endpoints";
import { httpRequest } from "@/service/http-request";

interface PaymentPayload {
    deviceData: string;
    clientToken: string;
}

export const useBraintreeSetup = (
    clientToken: string | null,
    callback: () => void,
    access_token: string | null
): boolean => {
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        let collectedDeviceData: string | undefined;

        if (clientToken) {
            braintreeWeb.client.create(
                {
                    authorization: clientToken,
                },
                (errorCreatingClient: any, clientInstance: any) => {
                    if (errorCreatingClient) {
                        console.log("Error creating client instance:", errorCreatingClient);
                        return;
                    }

                    braintreeWeb.dataCollector.create(
                        {
                            client: clientInstance,
                        },
                        (errorCollectingData: any, dataCollector: any) => {
                            if (errorCollectingData) {
                                console.log("Error collecting data:", errorCollectingData);
                                return;
                            }
                            collectedDeviceData = dataCollector.deviceData;
                        }
                    );

                    braintreeWeb.paypalCheckout.create(
                        {
                            client: clientInstance,
                        },
                        (errorCreatingPaypal: any, paypalCheckout: any) => {
                            if (errorCreatingPaypal) {
                                console.log("Error creating PayPal Checkout instance:", errorCreatingPaypal);
                                return;
                            }

                            paypalCheckout.loadPayPalSDK(
                                {
                                    vault: true,
                                },
                                () => {
                                    const paypalSDK = (window as any).paypal;
                                    paypalSDK
                                        .Buttons({
                                            fundingSource: paypalSDK.FUNDING.PAYPAL,

                                            createBillingAgreement: () => {
                                                return paypalCheckout.createPayment({ flow: "vault" });
                                            },

                                            onApprove: (approvalData: any, actions: any) => {
                                                return paypalCheckout.tokenizePayment(
                                                    approvalData,
                                                    (tokenizeError: any, paymentPayload: PaymentPayload) => {
                                                        if (tokenizeError) {
                                                            console.log("Error tokenizing payment:", tokenizeError);
                                                            return;
                                                        }
                                                        paymentPayload.deviceData = collectedDeviceData!;
                                                        paymentPayload.clientToken = clientToken;

                                                        httpRequest(
                                                            paymentEndpoints.CompleteBraintreeSetup.Uri,
                                                            paymentEndpoints.CompleteBraintreeSetup.Method,
                                                            paymentPayload,
                                                            undefined,
                                                            access_token || ""
                                                        ).then(() => callback());
                                                    }
                                                );
                                            },

                                            onCancel: (cancelData: any) => {
                                                console.log("Payment cancelled:", cancelData);
                                            },

                                            onError: (error: any) => {
                                                console.log("Payment error:", error);
                                            },
                                        })
                                        .render("#paypal-button-container")
                                        .then(() => {
                                            setIsLoading(false);
                                        });
                                }
                            );
                        }
                    );
                }
            );
        }
    }, [clientToken, callback, access_token]);

    return isLoading;
};
