namespace POD.Functions.Shopify.CreateWebhooks.Services.Interfaces
{
    public interface IStoreService
    {
        // Store Seller Use Case changed. When Implementing more webhooks, refactor this
        
        public Task<bool> SetShopifyScopeUpdatedTrue(string shop);
    }
}