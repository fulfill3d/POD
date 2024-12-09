using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POD.Common.Service.Interfaces;
using POD.Functions.Shopify.AppInstall.Service.Options;
using POD.Functions.Shopify.AppInstall.Services.Interfaces;

namespace POD.Functions.Shopify.AppInstall.Services
{
    public class ConfigurationService(
        ICommonConfigurationService commonConfigurationService,
        IOptions<ConfigurationNames> conf,
        ILogger<ConfigurationService> logger) : IConfigurationService
    {
        private readonly string _installEmailConfigName = conf.Value.InstallEmailConfigName;

        public async Task<string> GetInstallEmailAsync()
        {
            var config = await commonConfigurationService.GetByNameAsync(_installEmailConfigName);

            if (config != null && !string.IsNullOrEmpty(config.Configuration1))
            {
                return config.Configuration1;
            }

            var exMessage = "New install message cannot be found";
            logger.LogError(exMessage);
            throw new Exception(exMessage);
        }
    }
}