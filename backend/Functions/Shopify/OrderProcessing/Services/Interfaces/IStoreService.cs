using POD.Functions.Shopify.OrderProcessing.Data.Models.Shopify;

namespace POD.Functions.Shopify.OrderProcessing.Services.Interfaces
{
    public interface IStoreService
    {
        public Task<POD.Common.Database.Models.Store> GetStoreByShopIdentifier(string shop, Order newOrder);
        // public Task<int> GetCustomerId(string shop, Order newOrder);
        //
        // public Task<POD.Common.Database.Models.Seller> GetCustomer(string shop, Order newOrder);
    }
}
