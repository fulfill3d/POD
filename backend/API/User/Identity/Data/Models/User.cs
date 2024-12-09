using Newtonsoft.Json;

namespace POD.API.User.Identity.Data.Models
{
    public class User
    {
        [JsonProperty("family_name")]
        public string FamilyName { get; set; }
        
        [JsonProperty("given_name")]
        public string GivenName { get; set; }
        
        [JsonProperty("oid")]
        public string OID { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}