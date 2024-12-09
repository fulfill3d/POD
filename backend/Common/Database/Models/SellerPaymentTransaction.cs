using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class SellerPaymentTransaction
    {
        public SellerPaymentTransaction()
        {
            BraintreeTransactionDetails = new HashSet<BraintreeTransactionDetail>();
            PayPalTransactionDetails = new HashSet<PayPalTransactionDetail>();
            StoreSaleTransactions = new HashSet<StoreSaleTransaction>();
            StripeTransactionDetails = new HashSet<StripeTransactionDetail>();
        }

        public int Id { get; set; }
        public decimal Total { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int SellerPaymentMethodId { get; set; }
        public Guid UserRefId { get; set; }

        public virtual SellerPaymentMethod SellerPaymentMethod { get; set; } = null!;
        public virtual ICollection<BraintreeTransactionDetail> BraintreeTransactionDetails { get; set; }
        public virtual ICollection<PayPalTransactionDetail> PayPalTransactionDetails { get; set; }
        public virtual ICollection<StoreSaleTransaction> StoreSaleTransactions { get; set; }
        public virtual ICollection<StripeTransactionDetail> StripeTransactionDetails { get; set; }
    }
}
