import { CardCvcElement, CardExpiryElement, CardNumberElement, useElements, useStripe } from "@stripe/react-stripe-js";
import React, { forwardRef, useImperativeHandle, ForwardedRef } from "react";
import { useStripeSetupIntent } from "@/hooks/payment/use-stripe-setup-intent";
import { useStripeForm } from "@/hooks/payment/use-stripe-form";

export interface StripeCheckoutFormHandle extends HTMLFormElement {
    submitForm: () => Promise<
        { success: boolean; error: any; response: null } | { success: boolean; error: null; response: any }
    >;
}

const StripeCheckoutForm = forwardRef<StripeCheckoutFormHandle, {}>((props, ref) => {
    const stripe = useStripe();
    const elements = useElements();
    const stripeIntent = useStripeSetupIntent();
    const { isLoading, cardholderName, setCardholderName, submitForm } = useStripeForm(stripe, elements, stripeIntent);

    useImperativeHandle(ref, () => ({
        submitForm
    } as StripeCheckoutFormHandle));

    return (
        <form onSubmit={submitForm} ref={ref} className="relative">
            {isLoading && (
                <div className="absolute inset-0 flex justify-center items-center bg-white bg-opacity-80 z-10">
                    {/* Optional loading spinner can be added here */}
                </div>
            )}
            <div className="mb-5 p-3 border border-gray-300 rounded bg-white">
                <input
                    className="w-full border-none outline-none text-gray-800 text-lg leading-tight"
                    type="text"
                    placeholder="Cardholder Name"
                    value={cardholderName}
                    onChange={(e) => setCardholderName(e.target.value)}
                    required
                />
            </div>
            <div className="mb-5 p-3 border border-gray-300 rounded bg-white">
                <CardNumberElement options={CARD_ELEMENT_OPTIONS} />
            </div>
            <div className="flex space-x-4">
                <div className="flex-1 p-3 border border-gray-300 rounded bg-white">
                    <CardExpiryElement options={CARD_ELEMENT_OPTIONS} />
                </div>
                <div className="flex-1 p-3 border border-gray-300 rounded bg-white">
                    <CardCvcElement options={CARD_ELEMENT_OPTIONS} />
                </div>
            </div>
        </form>
    );
});

StripeCheckoutForm.displayName = "StripeCheckoutForm";

export { StripeCheckoutForm };

// Custom options for Stripe elements
const CARD_ELEMENT_OPTIONS = {
    style: {
        base: {
            color: "#32325d",
            fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
            fontSmoothing: "antialiased",
            fontSize: "16px",
            "::placeholder": {
                color: "#aab7c4",
            },
        },
        invalid: {
            color: "#fa755a",
            iconColor: "#fa755a",
        },
    },
    showIcon: true,
};
