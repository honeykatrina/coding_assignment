using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using UserAccountManagement.Shared.ServiceBusServices;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.TransactionModule.Models.Entities;
using UserAccountManagement.TransactionModule.Repositories;

namespace UserAccountManagement.TransactionModule.Services;

public class ProcessCreateTransactionMessageService: IProcessDataService<CreateTransaction>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepository;

    public ProcessCreateTransactionMessageService(
        IMapper mapper,
        IServiceProvider serviceProvider)
    {
        _mapper = mapper;
        _transactionRepository = serviceProvider.GetRequiredService<ITransactionRepository>();
    }

    public bool ProcessAsync(CreateTransaction message)
    {
        var transaction = _mapper.Map<Transaction>(message);
        _transactionRepository.Create(transaction);
        var createdTransaction = _transactionRepository.GetById(transaction.Id);

        return createdTransaction != null;
    }
}
