using POD.Integrations.ShopifyClient.Model.Product;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyProductClient
    {
        Task<RestResponse<Product>> CreateOrUpdate(Product product);

        Task<RestResponse<Product>> Get(long id);
    }
}
