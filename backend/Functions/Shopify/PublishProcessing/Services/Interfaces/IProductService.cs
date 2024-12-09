using POD.Functions.Shopify.PublishProcessing.Data.Models;

namespace POD.Functions.Shopify.PublishProcessing.Services.Interfaces
{
    public interface IProductService
    {
        Task<ShopifyProductMessage?> PrepareShopifyProductMessage(PublishProductMessage publishProductMessage);
        
        Task<ShopifyProductMessage> PrepareShopifyUpdateProductMessage(ShopifyProductMessage shopifyProductMessage);
    }
}