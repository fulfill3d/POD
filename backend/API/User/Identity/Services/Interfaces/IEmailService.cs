namespace POD.API.User.Identity.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string email, string firstName, string lastName);
        Task SendProfileUpdatedEmail(string email, string firstName, string lastName);
    }
}