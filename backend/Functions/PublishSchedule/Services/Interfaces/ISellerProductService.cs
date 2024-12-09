using POD.Functions.PublishSchedule.Data.Models;

namespace POD.Functions.PublishSchedule.Services.Interfaces
{
    public interface ISellerProductService
    {
        Task<IEnumerable<PublishProcessingProduct>> GetPublishProcessReadyProducts(
            int maxProduct,
            PublishProcessingRetryStrategy retryStrategy);
    }
}