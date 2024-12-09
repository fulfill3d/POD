using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreSaleOrderDetail
    {
        public StoreSaleOrderDetail()
        {
            StoreSaleTransactionDetails = new HashSet<StoreSaleTransactionDetail>();
        }

        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Quantity { get; set; }
        public string? OrderItemNumber { get; set; }
        public decimal? StorePrice { get; set; }
        public string? StoreProductId { get; set; }
        public string? StoreOrderLineItemId { get; set; }
        public string? StoreVariantTitle { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public int StoreSaleOrderId { get; set; }
        public int StoreProductVariantId { get; set; }

        public virtual StoreProductVariant StoreProductVariant { get; set; } = null!;
        public virtual StoreSaleOrder StoreSaleOrder { get; set; } = null!;
        public virtual ICollection<StoreSaleTransactionDetail> StoreSaleTransactionDetails { get; set; }
    }
}
