interface IStripeIntentResult {
    id: string;
    payment_method_types: string[];
    customer_id: string;
    created: Date;
    client_secret: string;
}

class StripeIntentResult implements IStripeIntentResult {
    constructor(
        public id: string,
        public payment_method_types: string[],
        public customer_id: string,
        public created: Date,
        public client_secret: string
    ) {}

    static fromJSON(data: any): StripeIntentResult {
        return new StripeIntentResult(
            data.id,
            data.payment_method_types,
            data.customer_id,
            new Date(data.created),
            data.client_secret
        );
    }
}
