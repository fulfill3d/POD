using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreSaleTransactionDetail
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public int StoreSaleTransactionId { get; set; }
        public int StoreSaleOrderDetailId { get; set; }

        public virtual StoreSaleOrderDetail StoreSaleOrderDetail { get; set; } = null!;
        public virtual StoreSaleTransaction StoreSaleTransaction { get; set; } = null!;
    }
}
