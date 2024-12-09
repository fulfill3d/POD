using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class BraintreeTransactionDetail
    {
        public int Id { get; set; }
        public string BraintreeTransactionId { get; set; } = null!;
        public int SellerPaymentTransactionId { get; set; }

        public virtual SellerPaymentTransaction SellerPaymentTransaction { get; set; } = null!;
    }
}
