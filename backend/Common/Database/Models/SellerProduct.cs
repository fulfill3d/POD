using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class SellerProduct
    {
        public SellerProduct()
        {
            SellerProductVariants = new HashSet<SellerProductVariant>();
        }

        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int SellerId { get; set; }
        public string? Tags { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? BodyHtml { get; set; }
        public string? Url { get; set; }
        public Guid UserRefId { get; set; }

        public virtual Seller Seller { get; set; } = null!;
        public virtual ICollection<SellerProductVariant> SellerProductVariants { get; set; }
    }
}
