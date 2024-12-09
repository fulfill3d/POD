using Microsoft.Extensions.Options;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Service.Options;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class AuthService(
        IHttpClientFactory httpClientFactory,
        ITokenService tokenService,
        ISellerService sellerService,
        IStoreService storeService,
        IShopifyRequest shopifyRequest,
        IUserService userService,
        IOptions<ShopifyApiEndpointsOptions> opt,
        IOptions<B2COptions> b2COptions,
        ILogger<AuthService> logger) : IAuthService
    {
        private readonly string _shopifySecretKey = opt.Value.ShopifySecretKey;
        private readonly string _frontEndUrl = opt.Value.PodFrontEndUrl;
        private readonly string _b2CAuthorizationEndpoint = b2COptions.Value.AuthorizationEndpoint;
        private readonly string _b2CTokenEndpoint = b2COptions.Value.TokenEndpoint;
        private readonly string _b2CClientId = b2COptions.Value.ClientId;
        private readonly string _b2CClientSecret = b2COptions.Value.ClientSecret;
        private readonly string _b2CRedirectUri = b2COptions.Value.RedirectUri;
        private readonly string _b2CNonce = b2COptions.Value.Nonce;
        private readonly string _b2CPolicy = b2COptions.Value.Policy;
        private readonly string _b2CResponseType = b2COptions.Value.ResponseType;
        private readonly string _b2CScope = b2COptions.Value.Scope;
        private readonly string _b2CGrantType = b2COptions.Value.GrantType;
        
        public async Task<string> Authentication(string storeName, string code)
        {
            var token = tokenService.GenerateRedirectToken(storeName, code);

            var urlBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = _b2CAuthorizationEndpoint
            };
            
            var query = System.Web.HttpUtility.ParseQueryString(urlBuilder.Query);
            
            query["p"] = _b2CPolicy;
            query["client_id"] = _b2CClientId;
            query["nonce"] = _b2CNonce;
            query["redirect_uri"] = _b2CRedirectUri;
            query["scope"] = _b2CScope;
            query["response_type"] = _b2CResponseType;
            query["state"] = token;
            
            urlBuilder.Query = query.ToString();
            
            return urlBuilder.ToString();
        }

        public async Task<string> PostAuthentication(string b2CAuthCode, string shopifyJwtToken)
        {
            var tokenResponse = await ExchangeCodeForTokenAsync(b2CAuthCode);
            if (tokenResponse == null)
            {
                return string.Empty;
            }
            
            var (storeName, storeCode) = tokenService.ValidateRedirectToken(shopifyJwtToken);
            var idToken = tokenResponse.Value<string>("id_token");
            if (string.IsNullOrEmpty(idToken))
            {
                return string.Empty;
            }

            var b2CUser = DecodeIdToken(idToken);

            logger.LogInformation(b2CUser.ToString());
            
            var user = await userService.NewOrExistingUser(b2CUser);
            var sellerId = await sellerService.NewOrExistingSeller(user);
            await storeService.NewOrExistingStore(user, sellerId, storeName, storeCode);

            var urlBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = _frontEndUrl
            };
                
            var query = System.Web.HttpUtility.ParseQueryString(urlBuilder.Query);
            urlBuilder.Query = query.ToString();
            
            return urlBuilder.ToString();
        }
        
        private async Task<JObject?> ExchangeCodeForTokenAsync(string code)
        {
            var httpClient = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _b2CTokenEndpoint);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", _b2CGrantType),
                new KeyValuePair<string, string>("client_id", _b2CClientId),
                new KeyValuePair<string, string>("client_secret", _b2CClientSecret),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", _b2CRedirectUri),
                new KeyValuePair<string, string>("scope", "openid")
            });
            request.Content = content;

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseString = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseString);
        }
        
        private static B2CUser DecodeIdToken(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken) as JwtSecurityToken;
            
            var payload = jsonToken.Payload;

            // TODO JWT Implicit conversion was buggy, Do not forget email at CustomPolicy
            var emailsString = "";
            if (payload.ContainsKey("emails"))
            {
                emailsString = payload["emails"].ToString() ?? string.Empty;
                if (emailsString != string.Empty)
                {
                    emailsString = emailsString.Trim('[', ']', '\n', ' ', '\r', '"');
                }
            }

            return new B2CUser
            {
                FamilyName = payload["family_name"].ToString() ?? string.Empty,
                GivenName = payload["given_name"].ToString() ?? string.Empty,
                OID = payload["oid"].ToString() ?? string.Empty,
                Email = emailsString ?? string.Empty
            };
        }
        
        public bool IsAuthenticRequest(NameValueCollection queryString)
        {
            return shopifyRequest.IsAuthenticRequest(queryString, _shopifySecretKey);
        }

        public bool IsAuthenticWebhook(string hmacHeaderValue, string requestBody)
        {
            return shopifyRequest.IsAuthenticWebhook(hmacHeaderValue, requestBody, _shopifySecretKey);
        }
    }
}
