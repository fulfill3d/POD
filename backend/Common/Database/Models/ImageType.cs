using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class ImageType
    {
        public ImageType()
        {
            SellerProductVariantImages = new HashSet<SellerProductVariantImage>();
            StoreProductVariantImages = new HashSet<StoreProductVariantImage>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public bool? IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public virtual ICollection<SellerProductVariantImage> SellerProductVariantImages { get; set; }
        public virtual ICollection<StoreProductVariantImage> StoreProductVariantImages { get; set; }
    }
}
