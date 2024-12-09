namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateRedirectToken(string shop, string code);
        (string shop, string code) ValidateRedirectToken(string token);
    }
}
