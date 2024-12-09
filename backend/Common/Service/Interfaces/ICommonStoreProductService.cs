using POD.Common.Core.Enum;
using POD.Common.Database.Models;

namespace POD.Common.Service.Interfaces
{
    public interface ICommonStoreProductService
    {
        Task<PublishStatus> GetStoreProductPublishStatus(int id);
        Task SetStoreProductPublishStatus(int id, PublishStatus status);
        Task<StoreProduct?> GetStoreProductById(int storeProductId);
        Task<int> GetStoreProductVariantIdBySku(string sku);
        Task<int> GetStoreProductIdByOnlineStoreProductId(string id);
        Task DeleteStoreProductByOnlineStoreId(string onlineStoreProductId);
        Task SetStoreProductOnlineStoreIdById(int storeProductId, string onlineStoreProductId);
        Task SetStoreProductVariantOnlineStoreProductVariantIdById(int storeProductVariantId, 
            string onlineStoreProductVariantId);
        Task DeleteAllStoreProductsByStoreId(int storeId);
    }
}