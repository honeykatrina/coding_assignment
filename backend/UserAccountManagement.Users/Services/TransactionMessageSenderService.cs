using Microsoft.Extensions.Options;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Shared.ServiceBusServices;

namespace UserAccountManagement.Users.Services;

public class TransactionMessageSenderService : IMessageSender
{
    private readonly IServiceBusSenderService _serviceBusSenderService;
    private readonly QueueSettings _appSettings;

    public TransactionMessageSenderService(
        IServiceBusSenderService serviceBusSenderService,
        IOptions<QueueSettings> options)
    {
        _serviceBusSenderService = serviceBusSenderService;
        _appSettings = options.Value;
    }

    public async Task SendMessageAsync(string message)
    {
        await _serviceBusSenderService.SendMessageToQueueAsync(message, _appSettings.Name);
    }
}