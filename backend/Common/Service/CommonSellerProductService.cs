using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;

namespace POD.Common.Service
{
    public class CommonSellerProductService(DatabaseContext dbContext) : ICommonSellerProductService
    {
        
        public async Task<POD.Common.Database.Models.SellerProduct> GetSellerProductById(int customerProductId)
        {
            return await dbContext.SellerProducts.FindAsync(customerProductId);
        }

        // public async Task<PublishStatus> GetPublishStatus(int customerProductId)
        // {
        //     var product = await GetSellerProductById(customerProductId);
        //
        //     return (PublishStatus)product.PublishingStatus;
        // }

        // public async Task SetPublishingStatus(int customerProductId, PublishStatus status)
        // {
        //     var product = await GetSellerProductById(customerProductId);
        //
        //     if (product == null) return;
        //
        //     product.PublishingStatus = (int)status;
        //     product.LastPublishingStatusChangeDate = DateTime.UtcNow;
        //
        //     await _dbContext.SaveChangesAsync();
        // }
    }
}