using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model
{
    public class ExchangeToken
    {
        [JsonProperty("client_id")] public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("grant_type")] public string GrantType { get; set; }
    
        [JsonProperty("subject_token")] public string SubjectToken { get; set; }
    
        [JsonProperty("subject_token_type")] public string SubjectTokenType { get; set; }
    
        [JsonProperty("requested_token_type")] public string RequestedTokenType { get; set; }
    }
}