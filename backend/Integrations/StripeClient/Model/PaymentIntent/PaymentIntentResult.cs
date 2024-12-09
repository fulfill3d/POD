namespace POD.Integrations.StripeClient.Model.PaymentIntent
{
    public class PaymentIntentResult
    {
        public string Id { get; set; }

        public long Amount { get; set; }
        
        public long AmountCapturable { get; set; }

        public long AmountReceived { get; set; }
        
        public string ConfirmationMethod { get; set; }

        public DateTime Created { get; set; }

        public string Currency { get; set; }

        public string CustomerId { get; set; }

        public string PaymentMethodId { get; set; }

        public string Status { get; set; }
    }
}
