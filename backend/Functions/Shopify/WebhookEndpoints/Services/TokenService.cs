using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using POD.Functions.Shopify.WebhookEndpoints.Service.Options;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class TokenService(IOptions<TokenOptions> options) : ITokenService
    {
        private readonly string _secret = options.Value.ShopifyOauthHashSalt;

        public string GenerateRedirectToken(string shop, string code)
        {    
            var symmetricKey = Convert.FromBase64String(_secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("shop", shop),
                            new Claim("code", code)
                        }),

                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(30)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var sToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(sToken);
        }
        
        public (string shop, string code) ValidateRedirectToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(_secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken)
                {
                    var shop = principal.Claims.FirstOrDefault(x => x.Type == "shop")?.Value;
                    var code = principal.Claims.FirstOrDefault(x => x.Type == "code")?.Value;

                    if (shop != null && code != null)
                    {
                        return (shop, code);
                    }
                    else
                    {
                        throw new SecurityTokenValidationException("Invalid token claims.");
                    }
                }
                else
                {
                    throw new SecurityTokenValidationException("Invalid token.");
                }
            }
            catch (Exception ex)
            {
                // Handle validation failure (e.g., log the exception)
                throw new SecurityTokenValidationException("Token validation failed.", ex);
            }
        }
    }
}
