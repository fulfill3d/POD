using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;

namespace POD.Common.Service
{
    public class CommonConfigurationService(DatabaseContext dbContext): ICommonConfigurationService
    {
        public async Task<Configuration> GetByNameAsync(string name)
        {
            return await dbContext.Configurations
                .Where(c => c.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}