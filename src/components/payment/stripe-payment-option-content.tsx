import { loadStripe } from "@stripe/stripe-js";
import { Elements } from "@stripe/react-stripe-js";
import React, { useCallback, useRef } from "react";
import {StripeCheckoutForm, StripeCheckoutFormHandle} from "./stripe-checkout-form";

export function StripePaymentOptionContent() {
    const stripePromise = loadStripe(process.env.NEXT_PUBLIC_REACT_APP_STRIPE_PK || "");

    const checkoutFormRef = useRef<StripeCheckoutFormHandle>(null);

    const handleButtonClick = useCallback(async () => {
        if (checkoutFormRef.current) {
            try {
                const result = await checkoutFormRef.current.submitForm();
                if (result.success) {
                    console.log('result.response', result.response);
                } else {
                    console.log('result.error?.message', result.error?.message);
                }
            } catch (err) {
                console.log('err', err);
            }
        } else {
            console.log('error')
        }
    }, [checkoutFormRef]);

    return (
        <div>
            <Elements stripe={stripePromise}>
                <StripeCheckoutForm ref={checkoutFormRef}/>
            </Elements>
            <button style={{ width: '100px', height: '20px' }} onClick={handleButtonClick}/>
        </div>
    );
}
