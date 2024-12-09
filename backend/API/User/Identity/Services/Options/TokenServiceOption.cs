namespace POD.API.User.Identity.Services.Options
{
    public class TokenServiceOption
    {        
        public string TokenEndpoint { get; set; }
        public string UpdatePolicy { get; set; }
        public string SignInUpPolicy { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string PostRegisterRedirectUri { get; set; }
        public string PostUpdateRedirectUri { get; set; }
        public string GrantType { get; set; }
        public string Scope { get; set; }
    }
}