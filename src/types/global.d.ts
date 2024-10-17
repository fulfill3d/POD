interface PayPalSDK {
    FUNDING: {
        PAYPAL: string;
    };
    Buttons: (config: {
        fundingSource?: string;
        createBillingAgreement?: () => Promise<any>;
        onApprove?: (data: any, actions: any) => void;
        onCancel?: (data: any) => void;
        onError?: (error: any) => void;
    }) => {
        render: (selector: string) => Promise<void>;
    };
}

declare global {
    interface Window {
        paypal: PayPalSDK;
    }
}
