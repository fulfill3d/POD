namespace POD.Functions.Payment.Common.Data.Models
{
    public class StripeDetails : PaymentDetails
    {
        public string StripeSellerId { get; set; }

        public string StripePaymentId { get; set; }
    }
}
