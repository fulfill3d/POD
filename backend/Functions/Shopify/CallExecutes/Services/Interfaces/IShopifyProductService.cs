using POD.Functions.Shopify.CallExecutes.Data.Models;

namespace POD.Functions.Shopify.CallExecutes.Services.Interfaces
{
    public interface IShopifyProductService
    {
        Task<bool> ShopifyCreateOrUpdateProductAsync(ShopifyProductMessage message);
        Task<bool> ShopifyUpdateProduct(ShopifyProductMessage message);
    }
}
