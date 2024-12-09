using POD.Functions.Shopify.CallExecutes.Data.Models;

namespace POD.Functions.Shopify.CallExecutes.Services.Interfaces
{
    public interface IShopifyUpdateInventoryService
    {
        Task<bool> UpdateInventoryForProduct(UpdateInventoryForProductMessage createdProduct);
    }
}
