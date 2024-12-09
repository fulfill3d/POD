namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IBlobService
    {
        Task SaveShopifyOrderError(string uniqName , string blob);
    }
}
