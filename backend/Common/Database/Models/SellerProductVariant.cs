using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class SellerProductVariant
    {
        public SellerProductVariant()
        {
            ProductPieces = new HashSet<ProductPiece>();
            SellerProductVariantImages = new HashSet<SellerProductVariantImage>();
            StoreProductVariants = new HashSet<StoreProductVariant>();
        }

        public bool IsEnabled { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int SellerProductId { get; set; }
        public string? Name { get; set; }
        public string? Tags { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? Material { get; set; }
        public decimal? ShippingPrice { get; set; }
        public decimal? Weight { get; set; }
        public int Id { get; set; }
        public int WeightUnitId { get; set; }

        public virtual SellerProduct SellerProduct { get; set; } = null!;
        public virtual Unit WeightUnit { get; set; } = null!;
        public virtual ICollection<ProductPiece> ProductPieces { get; set; }
        public virtual ICollection<SellerProductVariantImage> SellerProductVariantImages { get; set; }
        public virtual ICollection<StoreProductVariant> StoreProductVariants { get; set; }
    }
}
