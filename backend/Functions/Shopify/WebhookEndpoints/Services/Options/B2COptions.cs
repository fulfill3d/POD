namespace POD.Functions.Shopify.WebhookEndpoints.Service.Options
{
    public class B2COptions
    {
        public string AuthorizationEndpoint { get; set; }
        public string TokenEndpoint { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Nonce { get; set; }
        public string Policy { get; set; }
        public string ResponseType { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }
    }
}