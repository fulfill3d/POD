using Microsoft.Extensions.Options;
using POD.Functions.PublishSchedule.Data.Models;
using POD.Functions.PublishSchedule.Services.Interfaces;
using POD.Functions.PublishSchedule.Services.Options;

namespace POD.Functions.PublishSchedule.Services
{
    public class PublishScheduleService(
        IServiceBusService serviceBusService,
        ISellerProductService sellerProductService,
        IOptions<PublishScheduleServiceOption> opt) : IPublishScheduleService
    {
        private readonly PublishProcessingRetryStrategy _retryStrategy = new PublishProcessingRetryStrategy {
            MaxRetryInterval = opt.Value.MaxRetryInterval,
            MinRetryInterval = opt.Value.MinRetryInterval,
            MaxRetryCount = opt.Value.MaxRetryCount
        };
        
        private readonly int _maxProduct = opt.Value.MaxProducts;

        public async Task TriggerPublishProcessing()
        {
            var products = await sellerProductService
                .GetPublishProcessReadyProducts(_maxProduct, _retryStrategy);
            
            var tasks = products
                .Select(serviceBusService.SendStartPublishProcessingMessage)
                .ToList();
            
            await Task.WhenAll(tasks);
        }
    }
}