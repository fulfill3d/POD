using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StripeTransactionDetail
    {
        public int Id { get; set; }
        public string StripePaymentId { get; set; } = null!;
        public int SellerPaymentTransactionId { get; set; }

        public virtual SellerPaymentTransaction SellerPaymentTransaction { get; set; } = null!;
    }
}
