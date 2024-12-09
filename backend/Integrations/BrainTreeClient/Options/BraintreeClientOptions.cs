namespace POD.Integrations.BrainTreeClient.Options
{
    public class BraintreeClientOptions
    {
        public bool UseSandbox {get; set; }
        public string MerchantId {get; set; }
        public string PublicKey {get; set; }
        public string PrivateKey {get; set; }
    }
}