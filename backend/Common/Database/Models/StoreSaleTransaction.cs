using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreSaleTransaction
    {
        public StoreSaleTransaction()
        {
            StoreSaleTransactionDetails = new HashSet<StoreSaleTransactionDetail>();
        }

        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public int SellerPaymentTransactionId { get; set; }

        public virtual SellerPaymentTransaction SellerPaymentTransaction { get; set; } = null!;
        public virtual ICollection<StoreSaleTransactionDetail> StoreSaleTransactionDetails { get; set; }
    }
}
