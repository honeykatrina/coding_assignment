using UserAccountManagement.Shared.Models;

namespace UserAccountManagement.Shared.ServiceBusServices;

public interface IMessageSenderTypeFactory
{
    IMessageSender Create(MessageType messageType);
}
