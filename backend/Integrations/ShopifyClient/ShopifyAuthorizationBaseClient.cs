using Microsoft.Extensions.Logging;
using RestSharp;
using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient
{
    public abstract class ShopifyAuthorizationBaseClient
    {
        private const int MaxRetryCount = 2;
        private readonly ILogger _logger;
        private readonly RestClient _client;

        protected ShopifyAuthorizationBaseClient(string shop, ILogger logger)
        {
            var baseUrl = $"https://{shop}/admin/oauth/access_token";
            var options = new RestClientOptions(baseUrl)
            {
                ThrowOnAnyError = false,
                Timeout = TimeSpan.FromMinutes(5) // 5 minutes timeout
            };

            _client = new RestClient(options);
            _client.AddDefaultHeader("Content-Type", "application/json");
            _logger = logger;
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

                _logger.LogError($"Shopify API Failed {retryCount}");
                _logger.LogError(response.StatusCode.ToString());
                _logger.LogError(response.StatusDescription);
                _logger.LogError(response.ErrorException, response.ErrorMessage);

                retryCount++;

                isThereConflict = WaitAccordingToResponse(response.StatusCode);

                if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity) break;
            }
            while (retryCount <= MaxRetryCount || isThereConflict);

            return response;
        }

        private bool WaitAccordingToResponse(System.Net.HttpStatusCode responseCode)
        {
            switch (responseCode)
            {
                case System.Net.HttpStatusCode.Conflict:
                    _logger.LogInformation("There are conflicts with ongoing process.");
                    Thread.Sleep(10000);
                    return true;

                case System.Net.HttpStatusCode.BadGateway:
                    _logger.LogInformation("Bad Gateway error.");
                    Thread.Sleep(5000);
                    return false;

                case System.Net.HttpStatusCode.TooManyRequests:
                    _logger.LogInformation("Too many requests occurred. Sleeping for 2 sec.");
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

    }
}
