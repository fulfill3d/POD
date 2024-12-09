namespace POD.Integrations.StripeClient.Model.PaymentMethod
{
    public class Card
    {
        public string Brand { get; set; }
        public string Country { get; set; }
        public string Last4 { get; set; }
        public string Name { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
    }
}