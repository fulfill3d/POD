using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class SellerPaymentMethod
    {
        public SellerPaymentMethod()
        {
            BraintreeDetails = new HashSet<BraintreeDetail>();
            PaypalDetails = new HashSet<PaypalDetail>();
            SellerPaymentTransactions = new HashSet<SellerPaymentTransaction>();
            StripeDetails = new HashSet<StripeDetail>();
        }

        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int SellerId { get; set; }
        public int PaymentMethodId { get; set; }
        public Guid UserRefId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; } = null!;
        public virtual Seller Seller { get; set; } = null!;
        public virtual ICollection<BraintreeDetail> BraintreeDetails { get; set; }
        public virtual ICollection<PaypalDetail> PaypalDetails { get; set; }
        public virtual ICollection<SellerPaymentTransaction> SellerPaymentTransactions { get; set; }
        public virtual ICollection<StripeDetail> StripeDetails { get; set; }
                        
        public string GetPlaceHolder()
        {
            if (PaymentMethodId == (int)Common.Core.Enum.PaymentMethod.Stripe)
            {
                return StripeDetails.FirstOrDefault().PlaceHolder;
            }
            if (PaymentMethodId == (int)Common.Core.Enum.PaymentMethod.Braintree)
            {
                return BraintreeDetails.FirstOrDefault().PlaceHolder;
            }
            return null;
        }

        public string GetExpireDate()
        {
            if (PaymentMethodId == (int)Common.Core.Enum.PaymentMethod.Stripe)
            {
                return StripeDetails.FirstOrDefault().ExpireDate;
            }

            return null;
        }

        public string GetCardholderName()
        {
            if (PaymentMethodId == (int)Common.Core.Enum.PaymentMethod.Stripe)
            {
                return StripeDetails.FirstOrDefault().CardholderName;
            }

            if (PaymentMethodId == (int)Common.Core.Enum.PaymentMethod.Braintree)
            {
                return BraintreeDetails.FirstOrDefault().CardholderName;
            }

            return null;
        }
    }
}
