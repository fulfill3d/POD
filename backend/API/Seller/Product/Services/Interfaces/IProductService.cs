using POD.API.Common.Core;
using POD.API.Seller.Product.Data.Models;

namespace POD.API.Seller.Product.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<ProductView>> GetProducts(Guid userRefId, Pagination pagination);
        Task<ProductView?> GetProduct(Guid userRefId, int sellerProductId);
        Task<bool> CreateProduct(Guid userRefId, int sellerId, ProductRequest request);
        Task<bool> UpdateProduct(Guid userRefId, ProductRequest request);
        Task<bool> DeleteProduct(Guid userRefId, int sellerProductId);
        
        Task<bool> PostVariantToExistingProduct(Guid userRefId, int sellerProductId, ProductVariantRequest request);
        Task<bool> UpdateVariant(Guid userRefId, ProductVariantRequest request);
        Task<bool> DeleteVariant(Guid userRefId, int variantId);
    }
}