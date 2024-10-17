export const msalConfig = {
    auth: {
        clientId: process.env.NEXT_PUBLIC_B2C_CLIENT_ID || "",
        authority: process.env.NEXT_PUBLIC_B2C_AUTHORITY || "",
        redirectUri: process.env.NEXT_PUBLIC_B2C_REDIRECT_URI || "",
        knownAuthorities: [process.env.NEXT_PUBLIC_B2C_INSTANCE || ""],
    },
    cache: {
        cacheLocation: process.env.NEXT_PUBLIC_B2C_CACHE_LOCATION || "",
        storeAuthStateInCookie: true,
    }
};

export const loginRequest = {
    scopes: [
        process.env.NEXT_PUBLIC_B2C_SCOPE_OPENID || "",
        process.env.NEXT_PUBLIC_B2C_SCOPE_OFFLINE_ACCESS || "",
    ]
};

export const businessScopes = [
    process.env.NEXT_PUBLIC_B2C_SCOPE_BUSINESS_APPOINTMENT_READ || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_BUSINESS_APPOINTMENT_WRITE || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_BUSINESS_EMPLOYEE_READ || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_BUSINESS_EMPLOYEE_WRITE || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_BUSINESS_SERVICE_READ || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_BUSINESS_SERVICE_WRITE || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_BUSINESS_STORE_READ || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_BUSINESS_STORE_WRITE || "",
]

export const clientScopes = [
    process.env.NEXT_PUBLIC_B2C_SCOPE_CLIENT_SERVICE_READ || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_CLIENT_SERVICE_WRITE || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_CLIENT_APPOINTMENT_READ || "",
    process.env.NEXT_PUBLIC_B2C_SCOPE_CLIENT_APPOINTMENT_WRITE || ""
]
