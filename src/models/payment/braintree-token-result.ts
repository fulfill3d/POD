interface IBraintreeTokenResult {
    client_token?: string;
}

class BraintreeTokenResult implements IBraintreeTokenResult {
    constructor(public client_token?: string) {}

    static fromJSON(data: any): BraintreeTokenResult {
        return new BraintreeTokenResult(data.client_token);
    }
}
