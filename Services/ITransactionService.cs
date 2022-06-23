using UserAccountManagement.Models.Responses;

namespace UserAccountManagement.Services;

public interface ITransactionService
{
    BaseResponse<List<TransactionResponseModel>> GetTransactionsByAccountId(Guid accountId);
}