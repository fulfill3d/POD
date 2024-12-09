using Newtonsoft.Json.Linq;

namespace POD.API.User.Identity.Services.Interfaces
{
    public interface ITokenService
    {
        Task<JObject?> ExchangeCodeForTokenAsync(string code, bool update = false);
    }
}