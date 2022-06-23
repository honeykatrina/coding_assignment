using AutoMapper;
using UserAccountManagement.Helpers;
using UserAccountManagement.Models.Responses;
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

    public BaseResponse<List<TransactionResponseModel>> GetTransactionsByAccountId(Guid accountId)
    {
        var transactions = _transactionRepository.GetByAccountId(accountId);
        return new BaseResponseBuilder<List<TransactionResponseModel>>()
            .BuildSuccessResponse(_mapper.Map<List<TransactionResponseModel>>(transactions));
    }
}