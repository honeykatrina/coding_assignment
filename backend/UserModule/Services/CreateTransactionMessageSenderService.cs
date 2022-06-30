﻿using Microsoft.Extensions.Options;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Shared.ServiceBusServices;

namespace UserAccountManagement.UserModule.Services;

public class CreateTransactionMessageSenderService : IMessageSender
{
    private readonly IServiceBusSenderService _serviceBusSenderService;
    private readonly QueueSettings _appSettings;

    public CreateTransactionMessageSenderService(
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