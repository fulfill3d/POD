using POD.Functions.PublishSchedule.Data.Models;

namespace POD.Functions.PublishSchedule.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendStartPublishProcessingMessage(PublishProcessingProduct product);
    }
}