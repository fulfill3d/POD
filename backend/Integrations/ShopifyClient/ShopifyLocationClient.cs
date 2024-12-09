using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyLocationClient(
        string store,
        string accessToken,
        ILogger<ShopifyLocationClient> logger) : ShopifyBasicClient<Location>(store, accessToken, "locations", logger), IShopifyLocationClient
    {
    }
}
