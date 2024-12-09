using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class PaypalDetail
    {
        public int Id { get; set; }
        public string PayPalUserEmail { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string CorrelationId { get; set; } = null!;
        public string BillingAgreementId { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int SellerPaymentMethodId { get; set; }

        public virtual SellerPaymentMethod SellerPaymentMethod { get; set; } = null!;
    }
}
