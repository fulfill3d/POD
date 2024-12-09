using Microsoft.Extensions.Logging;
using RestSharp;
using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient
{
    public abstract class ShopifyBaseClient
    {
        private const string ApiVersion = "2023-04";
        private const int MaxRetryCount = 2;

        protected virtual bool SupportsApiVersioning => true;
        protected readonly ILogger Logger;
        private readonly RestClient _client;

        protected ShopifyBaseClient(string shop, string accessToken, ILogger logger)
        {
            Logger = logger;
            var baseUrl = DetermineBaseUrl(shop);
            
            var options = new RestClientOptions(baseUrl)
            {
                ThrowOnAnyError = false,
                Timeout = TimeSpan.FromMinutes(5) // 5 minutes timeout
            };

            _client = new RestClient(options);
            _client.AddDefaultHeader("X-Shopify-Access-Token", accessToken);
            _client.AddDefaultHeader("Content-Type", "application/json");
        }

        protected async Task<RestResponse<T>> ExecuteWithPolicyAsync<T>(RestRequest request)
        {
            var retryCount = 0;
            RestResponse<T> response;
            bool isThereConflict;
            do
            {
                response = await _client.ExecuteAsync<T>(request);

                if (response.IsSuccessful)
                {
                    return response;
                }

                Logger.LogError($"Shopify API Failed {retryCount}");
                Logger.LogError(response.StatusCode.ToString());
                Logger.LogError(response.StatusDescription);
                Logger.LogError(response.ErrorException, response.ErrorMessage);

                retryCount++;

                isThereConflict = WaitAccordingToResponse(response.StatusCode);

                if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity) break;
            }
            while (retryCount <= MaxRetryCount || isThereConflict);

            return response;
        }

        public async Task<RestResponse> ExecuteWithPolicyAsync(RestRequest request)
        {
            return await ExecuteWithPolicyAsync<string>(request);
        }

        private bool WaitAccordingToResponse(System.Net.HttpStatusCode responseCode)
        {
            switch (responseCode)
            {
                case System.Net.HttpStatusCode.Conflict:
                    Logger.LogInformation("There are conflicts with ongoing process.");
                    Thread.Sleep(10000);
                    return true;

                case System.Net.HttpStatusCode.BadGateway:
                    Logger.LogInformation("Bad Gateway error.");
                    Thread.Sleep(5000);
                    return false;

                case System.Net.HttpStatusCode.TooManyRequests:
                    Logger.LogInformation("Too many requests occurred. Sleeping for 2 sec.");
                    Thread.Sleep(2000);
                    return false;

                default:
                    return false;
            }
        }

        protected static RestRequest CreateRequest(string resource, Method method, object? data = null)
        {
            var request = new RestRequest(resource, method);

            if (data == null)
            {
                return request;
            }

            var jsonData = JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            return request;
        }
        
        private string DetermineBaseUrl(string shop)
        {
            return SupportsApiVersioning ? $"https://{shop}/admin/api/{ApiVersion}/" : $"https://{shop}/admin/";
        }
    }
}
