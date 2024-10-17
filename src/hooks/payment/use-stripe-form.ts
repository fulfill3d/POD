import {CardNumberElement} from "@stripe/react-stripe-js";
import React, {useCallback, useState} from "react";
import {paymentEndpoints} from "@/utils/endpoints";
import useHttp from "../common/use-http";
import {useAccessToken} from "@/msal/use-access-token";
import {Stripe, StripeElements} from "@stripe/stripe-js";
import {StripeIntentType} from "@/hooks/payment/use-stripe-setup-intent";

export const useStripeForm = (stripe: Stripe | null, elements: StripeElements | null, stripeIntent: StripeIntentType) => {
    const accessToken = useAccessToken();
    const { loading, error, request } = useHttp();
    const [isLoading, setIsLoading] = useState(false);
    const [cardholderName, setCardholderName] = useState('');

    const handleSubmit = useCallback(async (event: Event) => {
        event.preventDefault();

        if (!stripe || !elements || !stripeIntent) {
            return {
                success: false,
                error: {message: "Stripe not initialized"},
                response: null
            };
        }

        const cardElement = elements.getElement(CardNumberElement);

        if (!cardElement) {
            return {
                success: false,
                error: {message: "Card element not found"},
                response: null
            };
        }

        try {
            const { error, setupIntent } = await stripe.confirmCardSetup(stripeIntent.clientSecret, {
                payment_method: {
                    card: cardElement,
                    billing_details: {
                        name: cardholderName
                    },
                },
            });

            if (error) {
                return {
                    success: false,
                    error: error,
                    response: null
                };
            } else {
                const setupIntentData = {
                    client_secret: setupIntent.client_secret,
                    id: setupIntent.id,
                    payment_method: setupIntent.payment_method,
                    status: setupIntent.status,
                    usage: setupIntent.usage,
                };

                const response = await request(
                    paymentEndpoints.CompleteStripeSetup.Uri,
                    paymentEndpoints.CompleteStripeSetup.Method,
                    setupIntentData,
                    undefined,
                    accessToken
                );

                return {
                    success: true,
                    error: null,
                    response: response
                };
            }
        } catch (error) {
            return {
                success: false,
                error: error,
                response: null
            };
        }

    }, [stripe, elements, stripeIntent, cardholderName, request, accessToken]);

    const submitForm = useCallback(async () => {
        setIsLoading(true);
        const event = new Event('submit', {cancelable: true, bubbles: true});
        try {
            return await handleSubmit(event);
        } catch (error) {
            return {
                success: false,
                error: error,
                response: null
            };
        } finally {
            setIsLoading(false);
        }
    }, [handleSubmit]);

    return { isLoading, cardholderName, setCardholderName, submitForm };
};
