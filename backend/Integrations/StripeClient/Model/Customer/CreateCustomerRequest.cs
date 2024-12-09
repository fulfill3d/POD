namespace POD.Integrations.StripeClient.Model.Customer
{
    public class CreateCustomerRequest
    {
        public string Phone { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public CustomerAddress Address { get; set; }
    }
}