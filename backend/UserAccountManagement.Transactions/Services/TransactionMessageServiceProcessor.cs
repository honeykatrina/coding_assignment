using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Shared.ServiceBusServices;

namespace UserAccountManagement.TransactionModule.Services;

public class TransactionMessageProcessorHostedService : IHostedService
{
    private readonly IProcessDataService<CreateTransaction> _processDataService;
    private readonly QueueSettings _appSettings;
    private readonly ServiceBusClient _serviceBusClient;
    private ServiceBusProcessor _processor;

    public TransactionMessageProcessorHostedService(
        IProcessDataService<CreateTransaction> processDataService,
        ServiceBusClient serviceBusClient,
        IOptions<QueueSettings> options)
    {
        _appSettings = options.Value;
        _processDataService = processDataService;
        _serviceBusClient = serviceBusClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var options = new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 2
        };

        _processor = _serviceBusClient.CreateProcessor(_appSettings.Name, options);

        _processor.ProcessMessageAsync += ProcessMessageAsync;
        _processor.ProcessErrorAsync += ProcessErrorAsync;

        await _processor.StartProcessingAsync(cancellationToken);
    }

    private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
    {
        // TODO: Add logger.
        return Task.CompletedTask;
    }

    private async Task ProcessMessageAsync(ProcessMessageEventArgs arg)
    {
        var messageBody = arg.Message.Body.ToString();
        var deserialisedMessage = JsonSerializer.Deserialize<CreateTransaction>(messageBody);

        if (_processDataService.Process(deserialisedMessage))
            await arg.CompleteMessageAsync(arg.Message);
        else
            await arg.DeadLetterMessageAsync(arg.Message);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _processor.CloseAsync(cancellationToken: cancellationToken);
    }
}
