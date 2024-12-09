using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;
using POD.Integrations.ServiceBusClient.Options;
using Microsoft.Extensions.Options;

namespace POD.Integrations.ServiceBusClient
{
    public class ServiceBusClient(IOptions<ServiceBusClientOptions> configuration): IServiceBusClient
    {
        private readonly ServiceBusClientOptions _options = configuration.Value;
        
        public async Task SendMessage(ServiceBusClientMessage message)
        {
            await SendServiceBusMessage(message);
        }

        public async Task SendMessageWithTimeout(ServiceBusClientMessage message, CancellationToken cancellationToken)
        {
            await message.SendMessageWithTimeout(_options, cancellationToken);
        }

        private async Task SendServiceBusMessage(ServiceBusClientMessage message)
        {
            await message.SendMessage(_options);
        }
    }
}