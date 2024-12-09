using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class BraintreeDetail
    {
        public int Id { get; set; }
        public string CardholderName { get; set; } = null!;
        public string ClientToken { get; set; } = null!;
        public string BraintreeSellerId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string BillingAgreementId { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string PlaceHolder { get; set; } = null!;
        public string Tenant { get; set; } = null!;
        public string DeviceData { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int SellerPaymentMethodId { get; set; }

        public virtual SellerPaymentMethod SellerPaymentMethod { get; set; } = null!;
    }
}
