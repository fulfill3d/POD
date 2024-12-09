namespace POD.Functions.Shopify.AppUninstall.Services.Interfaces
{
    public interface IStoreService
    {
        // Store - Seller | Seller - Store Strategy changed after migration 06132024
        // public Task<bool> DeleteByShop(string shop);
        // public Task<POD.Common.Database.Models.Seller> GetByShop(string shop);

        public Task<POD.Common.Database.Models.Store> Get(string store);
        public Task<bool> Delete(POD.Common.Database.Models.Store store);
    }
}