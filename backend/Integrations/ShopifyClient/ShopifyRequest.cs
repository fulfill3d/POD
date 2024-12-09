using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyRequest : IShopifyRequest
    {
        public bool IsAuthenticRequest(NameValueCollection queryString, string shopifySecretKey)
        {
            var hmac = queryString.Get("hmac") ?? string.Empty;

            if (string.IsNullOrEmpty(hmac))
            {
                return false;
            }

            var kvps = queryString.Cast<string>()
                .Select(s => new { Key = ReplaceChars(s, true), Value = ReplaceChars(queryString[s], false) })
                .Where(kvp => kvp.Key != "signature" && kvp.Key != "hmac")
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => $"{kvp.Key}={kvp.Value}");

            var hmacHasher = new HMACSHA256(Encoding.UTF8.GetBytes(shopifySecretKey));
            var hash = hmacHasher.ComputeHash(Encoding.UTF8.GetBytes(string.Join("&", kvps)));

            var calculatedSignature = BitConverter.ToString(hash).Replace("-", "");

            return string.Equals(calculatedSignature, hmac, StringComparison.OrdinalIgnoreCase);

            string ReplaceChars(string? s, bool isKey)
            {
                var output = (s?.Replace("%", "%25").Replace("&", "%26")) ?? "";

                if (isKey)
                {
                    output = output.Replace("=", "%3D");
                }

                return output;
            }
        }
        
        public bool IsAuthenticWebhook(string hmacHeaderValue, string requestBody, string shopifySecretKey)
        {
            if (string.IsNullOrEmpty(hmacHeaderValue))
            {
                return false;
            }

            // Compute a hash from the shopifySecretKey and the request body
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(shopifySecretKey));
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody)));

            // Webhook is valid if the computed hash matches the header hash
            return hash == hmacHeaderValue;
        }

    }
}
