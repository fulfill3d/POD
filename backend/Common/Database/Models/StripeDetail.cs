using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StripeDetail
    {
        public int Id { get; set; }
        public string StripeSellerId { get; set; } = null!;
        public string StripeSetupIntentId { get; set; } = null!;
        public string StripePaymentMethodId { get; set; } = null!;
        public string PlaceHolder { get; set; } = null!;
        public string CardholderName { get; set; } = null!;
        public string ExpireDate { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int SellerPaymentMethodId { get; set; }

        public virtual SellerPaymentMethod SellerPaymentMethod { get; set; } = null!;
    }
}
