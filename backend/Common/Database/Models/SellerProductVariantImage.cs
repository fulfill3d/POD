using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class SellerProductVariantImage
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public bool? IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ImageTypeId { get; set; }
        public string? Name { get; set; }
        public string? Alt { get; set; }
        public int SellerProductVariantId { get; set; }
        public bool IsDefaultImage { get; set; }

        public virtual ImageType ImageType { get; set; } = null!;
        public virtual SellerProductVariant SellerProductVariant { get; set; } = null!;
    }
}
