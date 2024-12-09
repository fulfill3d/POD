using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyFulfillmentServiceClient(string shop, string token, ILogger logger) : ShopifyBasicClient<FulfillmentService>(shop, token, "fulfillment_services", logger), IShopifyFulfillmentServiceClient
    {
    }
}
