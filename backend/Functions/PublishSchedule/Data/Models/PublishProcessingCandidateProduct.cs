using POD.Common.Core.Enum;

namespace POD.Functions.PublishSchedule.Data.Models
{
    public class PublishProcessingCandidateProduct
    {
        public int StoreId { get; set; }
        public int StoreProductId { get; set; }
        public MarketPlace MarketPlace { get; set; }
        public int PublishRetryCount { get; set; }
        public DateTime LastPublishingStatusChangeDate { get; set; }
    }
}