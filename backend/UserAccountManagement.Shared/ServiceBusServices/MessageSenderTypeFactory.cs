using UserAccountManagement.Shared.Models;

namespace UserAccountManagement.Shared.ServiceBusServices;

public class MessageSenderTypeFactory: IMessageSenderTypeFactory
{
    private readonly Dictionary<MessageType, IMessageSender> _factories;

    public MessageSenderTypeFactory(IServiceProvider serviceProvider)
    {
        _factories = new Dictionary<MessageType, IMessageSender>();
        var notificators = serviceProvider.GetService(typeof(IEnumerable<IMessageSender>));
        var notificatorServices = (IMessageSender[])notificators;

        foreach (MessageType messageType in Enum.GetValues(typeof(MessageType)))
        {
            var type = Type.GetType($"UserAccountManagement.UserModule.Services.{messageType}MessageSenderService,UserAccountManagement.Users");
            var factory = notificatorServices.FirstOrDefault(x => x.GetType() == type);
            if (factory == null)
            {
                throw new ArgumentNullException(type.Name);
            }

            _factories.Add(messageType, factory);
        }
    }

    public IMessageSender Create(MessageType messageType) => _factories[messageType];
}
