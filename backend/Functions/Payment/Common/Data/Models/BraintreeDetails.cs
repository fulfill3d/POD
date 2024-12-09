namespace POD.Functions.Payment.Common.Data.Models
{
    public class BraintreeDetails : PaymentDetails
    {
        public string BraintreeSellerId { get; set; }

        public string PaymentMethodNonce { get; set; }

        public string DeviceData { get; set; }
    }
}