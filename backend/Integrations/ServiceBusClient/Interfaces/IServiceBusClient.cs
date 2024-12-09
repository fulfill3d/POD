using POD.Integrations.ServiceBusClient.Models;

namespace POD.Integrations.ServiceBusClient.Interfaces
{
    public interface IServiceBusClient
    {
        public Task SendMessage(ServiceBusClientMessage message);
        public Task SendMessageWithTimeout(ServiceBusClientMessage message, CancellationToken cancellationToken);
    }
}