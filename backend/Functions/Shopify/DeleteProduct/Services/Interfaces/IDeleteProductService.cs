using POD.Functions.Shopify.DeleteProduct.Data.Models;

namespace POD.Functions.Shopify.DeleteProduct.Services.Interfaces
{
    public interface IDeleteProductService
    {
        Task<bool> DeleteProducts(DeleteShopifyProductMessage request);
    }
}