namespace POD.Common.Service.Interfaces
{
    public interface ICommonConfigurationService
    {
        // TODO Database Entity Configuration here
        Task<Database.Models.Configuration> GetByNameAsync(string name);
    }
}