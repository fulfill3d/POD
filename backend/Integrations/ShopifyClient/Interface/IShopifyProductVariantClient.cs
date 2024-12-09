using POD.Integrations.ShopifyClient.Model.Product;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyProductVariantClient
    {
        Task<RestResponse<ProductVariant>> Update(long variantId, ProductVariant updateObject);
        Task<RestResponse<ProductVariant>> Get(long variantId);
    }
}
