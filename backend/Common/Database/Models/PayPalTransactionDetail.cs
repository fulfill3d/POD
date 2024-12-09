using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class PayPalTransactionDetail
    {
        public int Id { get; set; }
        public string PayPalCorrelationId { get; set; } = null!;
        public string PayPalTransactionId { get; set; } = null!;
        public int SellerPaymentTransactionId { get; set; }

        public virtual SellerPaymentTransaction SellerPaymentTransaction { get; set; } = null!;
    }
}
