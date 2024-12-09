using Microsoft.Azure.Functions.Worker.Http;

namespace POD.Common.Service.Interfaces
{
    public interface IJwtValidatorService
    {
        Task<string?> AuthenticateAndAuthorize(HttpRequestData req, string[] acceptedScopes);
    }
}