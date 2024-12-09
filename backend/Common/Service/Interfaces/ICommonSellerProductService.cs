using POD.Common.Core.Enum;
using POD.Common.Database.Models;

namespace POD.Common.Service.Interfaces
{
    public interface ICommonSellerProductService
    {
        // Task<PublishStatus> GetPublishStatus(int id);
        // Task SetPublishingStatus(int id, PublishStatus status);
        Task<SellerProduct> GetSellerProductById(int sellerProductId);
    }
}