using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreProductVariant
    {
        public StoreProductVariant()
        {
            StoreProductVariantImages = new HashSet<StoreProductVariantImage>();
            StoreSaleOrderDetails = new HashSet<StoreSaleOrderDetail>();
        }

        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int StoreProductId { get; set; }
        public string? Name { get; set; }
        public string? Tags { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? Material { get; set; }
        public decimal? ShippingPrice { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Price { get; set; }
        public int SellerProductVariantId { get; set; }
        public int WeightUnitId { get; set; }
        public string? OnlineStoreProductVariantId { get; set; }
        public string Sku { get; set; } = null!;

        public virtual SellerProductVariant SellerProductVariant { get; set; } = null!;
        public virtual StoreProduct StoreProduct { get; set; } = null!;
        public virtual Unit WeightUnit { get; set; } = null!;
        public virtual ICollection<StoreProductVariantImage> StoreProductVariantImages { get; set; }
        public virtual ICollection<StoreSaleOrderDetail> StoreSaleOrderDetails { get; set; }
    }
}
