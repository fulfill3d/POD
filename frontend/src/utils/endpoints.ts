import {HttpMethod} from "@/service/http-request";

interface Endpoint {
    Uri: string;
    Method: HttpMethod;
}

export const addressEndpoints = {
    GetAddresses: {
        Uri: `${process.env.NEXT_PUBLIC_SELLER_ADDRESS_BASE_URL}/api/get`,
        Method: HttpMethod.GET,
    } as Endpoint,
    GetAddress: (Id: number): Endpoint => ({
        Uri: `${process.env.NEXT_PUBLIC_SELLER_ADDRESS_BASE_URL}/api/get/${Id}`,
        Method: HttpMethod.GET,
    }) as Endpoint,
    AddAddress: {
        Uri: `${process.env.NEXT_PUBLIC_SELLER_ADDRESS_BASE_URL}/api/add`,
        Method: HttpMethod.POST,
    } as Endpoint,
    UpdateAddress: {
        Uri: `${process.env.NEXT_PUBLIC_SELLER_ADDRESS_BASE_URL}/api/update`,
        Method: HttpMethod.PATCH,
    } as Endpoint,
    DeleteAddress: (Id: number): Endpoint => ({
        Uri: `${process.env.NEXT_PUBLIC_SELLER_ADDRESS_BASE_URL}/api/delete/${Id}`,
        Method: HttpMethod.DELETE,
    }) as Endpoint,
};

export const paymentEndpoints = {
    GetPaymentMethods: {
        Uri: `${process.env.NEXT_PUBLIC_SELLER_PAYMENT_BASE_URL}/api/get/`,
        Method: HttpMethod.GET
    } as Endpoint,
    GetDefaultPaymentMethod: (Id: number): Endpoint => ({
        Uri: `${process.env.NEXT_PUBLIC_SELLER_PAYMENT_BASE_URL}/api/get/default`,
        Method: HttpMethod.GET
    }),
    SetDefaultPaymentMethod: (Id: number): Endpoint => ({
        Uri: `${process.env.NEXT_PUBLIC_SELLER_PAYMENT_BASE_URL}/api/set/default/${Id}`,
        Method: HttpMethod.PATCH
    }) as Endpoint,
    DeletePaymentMethod: (Id: number): Endpoint => ({
        Uri: `${process.env.NEXT_PUBLIC_SELLER_PAYMENT_BASE_URL}/api/delete/${Id}`,
        Method: HttpMethod.DELETE
    }) as Endpoint,
    GetBraintreeClientToken: {
        Uri: `${process.env.NEXT_PUBLIC_SELLER_PAYMENT_BASE_URL}/api/braintree/token/`,
        Method: HttpMethod.GET
    } as Endpoint,
    CompleteBraintreeSetup: {
        Uri: `${process.env.NEXT_PUBLIC_SELLER_PAYMENT_BASE_URL}/api/braintree/complete/`,
        Method: HttpMethod.POST
    } as Endpoint,
    GetStripeSetupIntent: {
        Uri: `${process.env.NEXT_PUBLIC_SELLER_PAYMENT_BASE_URL}/api/stripe/intent/`,
        Method: HttpMethod.GET
    } as Endpoint,
    CompleteStripeSetup: {
        Uri: `${process.env.NEXT_PUBLIC_SELLER_PAYMENT_BASE_URL}/api/stripe/complete/`,
        Method: HttpMethod.POST
    } as Endpoint,
}

export const productEndpoints = {
    CreateProduct: {
        Uri: `${process.env.NEXT_PUBLIC_SELLER_PRODUCT_BASE_URL}/api/seller/products/`,
        Method: HttpMethod.POST
    } as Endpoint,
};

export const storeEndpoints = {
    PublishProduct: (Id: number): Endpoint => ({
        Uri: `${process.env.NEXT_PUBLIC_SELLER_STORE_BASE_URL}/api/seller/store/${Id}/product/`,
        Method: HttpMethod.POST
    }) as Endpoint
};
