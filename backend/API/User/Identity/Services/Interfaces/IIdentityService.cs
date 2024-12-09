namespace POD.API.User.Identity.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> VerifyAndProcess(string code, bool update = false);
    }
}