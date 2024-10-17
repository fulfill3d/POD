interface ISellerPaymentMethod {
    seller_payment_method_id: number;
    payment_method_id: number;
    is_default: boolean;
    text: string;
    expire_date?: string;
    cardholder_name: string;
}

class SellerPaymentMethod implements ISellerPaymentMethod {
    constructor(
        public seller_payment_method_id: number,
        public payment_method_id: number,
        public is_default: boolean,
        public text: string,
        public cardholder_name: string,
        public expire_date?: string // Optional in the second constructor
    ) {}

    static fromJSON(data: any): SellerPaymentMethod {
        return new SellerPaymentMethod(
            data.seller_payment_method_id,
            data.payment_method_id,
            data.is_default,
            data.text,
            data.cardholder_name,
            data.expire_date // Optional expire_date
        );
    }
}
