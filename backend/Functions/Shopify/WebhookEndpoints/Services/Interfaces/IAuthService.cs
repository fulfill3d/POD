using System.Collections.Specialized;

namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authentication(string storeName, string code);
        Task<string> PostAuthentication(string b2CAuthCode, string shopifyJwtToken);
        bool IsAuthenticRequest(NameValueCollection queryString);
        bool IsAuthenticWebhook(string hmacHeaderValue, string requestBody);
    }
}
