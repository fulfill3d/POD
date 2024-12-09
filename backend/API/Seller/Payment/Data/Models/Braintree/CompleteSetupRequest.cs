using System;
namespace POD.API.Seller.Payment.Data.Models.Braintree
{
	public class CompleteSetupRequest
	{
		public string Nonce { get; set; }
		public string Type { get; set; }
		public string ClientToken { get; set; }
		public string DeviceData { get; set; }
		public SetupDetails Details { get; set; }
    }

    public class SetupDetails
    {
        public string BillingAgreementId { get; set; }
        public string Email { get; set; }
        public string Tenant { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}