namespace POD.Integrations.StripeClient.Model.Customer
{
    public class CustomerResult
    {
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Deleted { get; set; }
        public string Currency { get; set; }
        public DateTime Created { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}