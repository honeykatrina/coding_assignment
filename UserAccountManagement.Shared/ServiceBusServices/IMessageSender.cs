namespace UserAccountManagement.Shared.ServiceBusServices;

public interface IMessageSender
{
    Task SendMessageAsync(string message);
}
