using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model.Product;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyProductVariantClient(
        string store,
        string accessToken,
        ILogger<ShopifyProductVariantClient> logger) : ShopifyBasicClient<ProductVariant>(store, accessToken, "variants", logger), IShopifyProductVariantClient
    {
        
    }
}
