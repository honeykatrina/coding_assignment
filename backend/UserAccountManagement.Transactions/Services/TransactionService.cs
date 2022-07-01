using AutoMapper;
using UserAccountManagement.Shared.Helpers;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Transactions.Models.Responses;
using UserAccountManagement.Transactions.Repositories;

namespace UserAccountManagement.Transactions.Services;

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

    public BaseResponse<List<TransactionResponseModel>> GetTransactions()
    {
        var transactions = _transactionRepository.GetAll();

        return new BaseResponseBuilder<List<TransactionResponseModel>>()
            .BuildSuccessResponse(_mapper.Map<List<TransactionResponseModel>>(transactions));
    }
}