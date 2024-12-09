using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreProductVariantImage
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Alt { get; set; }
        public int ImageTypeId { get; set; }
        public int StoreProductVariantId { get; set; }
        public bool IsDefaultImage { get; set; }

        public virtual ImageType ImageType { get; set; } = null!;
        public virtual StoreProductVariant StoreProductVariant { get; set; } = null!;
    }
}
