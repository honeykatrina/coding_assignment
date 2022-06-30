using Azure.Messaging.ServiceBus;
namespace UserAccountManagement.Shared.ServiceBusServices;

public class ServiceBusSenderService: IServiceBusSenderService
{
    private readonly ServiceBusClient _serviceBusClient;

    public ServiceBusSenderService(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }

    public async Task SendMessageToQueueAsync(string message, string queueName)
    {
        await using var sender = _serviceBusClient.CreateSender(queueName);
        await sender.SendMessageAsync(new ServiceBusMessage(message));
    }
}
