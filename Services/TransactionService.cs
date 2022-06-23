using AutoMapper;
using UserAccountManagement.Models.ResponseModels;
using UserAccountManagement.Repositories;

namespace UserAccountManagement.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public List<TransactionResponseModel> GetTransactionsByAccountId(Guid accountId)
    {
        var transactions = _transactionRepository.GetByAccountId(accountId);
        return _mapper.Map<List<TransactionResponseModel>>(transactions);
    }
}