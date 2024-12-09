using POD.Common.Core.Enum;

namespace POD.Functions.PublishSchedule.Data.Models
{
    public class PublishProcessingProduct
    {
        public int StoreId { get; set; }
        public int StoreProductId { get; set; }
        public MarketPlace MarketPlace { get; set; }
    }
}