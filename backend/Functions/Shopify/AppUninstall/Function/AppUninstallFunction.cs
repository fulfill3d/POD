using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using POD.Functions.Shopify.AppUninstall.Services.Interfaces;

namespace POD.Functions.Shopify.AppUninstall
{
    public class AppUninstallFunction(IAppUninstallService appUninstallService)
    {
        [Function("shopify-uninstall")]
        public async Task Run(
            [ServiceBusTrigger("shopify-uninstall", Connection="ServiceBusConnectionString") ] string message,
            ILogger log)
        {
            log.LogInformation("Shopify Delete Products processing started.");
            log.LogInformation(message);

            try
            {
                var infoValues = ParseMessage(message);
                if (infoValues.Count > 1)
                {
                    await appUninstallService.ProcessUninstall(infoValues[0]);
                }
                else
                {
                    log.LogInformation("No info values found!");
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw;
            }

            log.LogInformation("Shopify Delete Products finished.");
        }

        private static IList<string> ParseMessage(string message)
        {
            return message.Split(new[] { "|-|" }, StringSplitOptions.None);
        }
    }
}