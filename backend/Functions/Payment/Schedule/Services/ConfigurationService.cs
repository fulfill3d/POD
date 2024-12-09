using POD.Common.Service.Interfaces;
using POD.Functions.Payment.Schedule.Services.Interfaces;

namespace POD.Functions.Payment.Schedule.Services
{
    public class ConfigurationService(
        ICommonConfigurationService commonConfigurationService, 
        string defaultPaymentMethodNotFoundEmailConfigurationName) : IConfigurationService
    {
        public async Task<string> GetDefaultPaymentMethodNotFoundMailMessageAsync()
        {
            var confObject = await commonConfigurationService
                .GetByNameAsync(defaultPaymentMethodNotFoundEmailConfigurationName);

            if (confObject == null) return string.Empty;

            return confObject.Configuration1;
        }
    }
}
