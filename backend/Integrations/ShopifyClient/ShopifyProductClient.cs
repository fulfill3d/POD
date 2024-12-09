using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model.Product;
using RestSharp;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyProductClient(
        string store,
        string accessToken,
        ILogger<ShopifyProductClient> logger) : ShopifyBasicClient<Product>(store, accessToken, "products", logger), IShopifyProductClient
    {
        public async Task<RestResponse<Product>> CreateOrUpdate(Product product)
        {
            if (product.Id.HasValue)
            {
                return await Update(product.Id.Value, product);
            }

            return await Create(product);
        }
    }
}
