namespace UserAccountManagement.Shared.ServiceBusServices;

public interface IServiceBusSenderService
{
    Task SendMessageToQueueAsync(string message, string queueName);
}
