using POD.API.Seller.Store.Data.Models;

namespace POD.API.Seller.Store.Services.Interfaces
{
    public interface IStoreService
    {
        Task<bool> CreateStoreProductBySellerProduct(int sellerId, int storeId, ProductRequest request);
    }
}