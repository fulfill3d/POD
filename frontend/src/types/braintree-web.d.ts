declare module 'braintree-web' {
    interface ClientCreateOptions {
        authorization: string;
    }

    interface DataCollectorCreateOptions {
        client: any;
    }

    interface PayPalCheckoutCreateOptions {
        client: any;
    }

    interface PayPalCheckoutInstance {
        loadPayPalSDK(options: { vault: boolean }, callback: () => void): void;
        createPayment(options: { flow: string }): Promise<any>;
        tokenizePayment(data: any, callback: (error: any, payload: any) => void): void;
    }

    interface DataCollectorInstance {
        deviceData: any;
    }

    namespace client {
        function create(options: ClientCreateOptions, callback: (error: any, clientInstance: any) => void): void;
    }

    namespace dataCollector {
        function create(options: DataCollectorCreateOptions, callback: (error: any, dataCollectorInstance: DataCollectorInstance) => void): void;
    }

    namespace paypalCheckout {
        function create(options: PayPalCheckoutCreateOptions, callback: (error: any, paypalCheckoutInstance: PayPalCheckoutInstance) => void): void;
    }
}
