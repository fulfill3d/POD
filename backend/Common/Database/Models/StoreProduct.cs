using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreProduct
    {
        public StoreProduct()
        {
            StoreProductVariants = new HashSet<StoreProductVariant>();
        }

        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int PublishingStatus { get; set; }
        public DateTime? LastPublishingStatusChangeDate { get; set; }
        public int? PublishRetryCount { get; set; }
        public int StoreId { get; set; }
        public string? Tags { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? BodyHtml { get; set; }
        public string? Url { get; set; }
        public string? ProductOnlineStoreId { get; set; }

        public virtual Store Store { get; set; } = null!;
        public virtual ICollection<StoreProductVariant> StoreProductVariants { get; set; }
    }
}
