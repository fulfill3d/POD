namespace POD.Integrations.StripeClient.Model.SetupIntent
{
    public class SetupIntentResult
    {
        public string Id { get; set; }
        public List<string> PaymentMethodTypes { get; set; }
        public string CustomerId { get; set; }
        public DateTime Created { get; set; }
        public string ClientSecret { get; set; }
    }
}