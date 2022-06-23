using UserAccountManagement.Models.ResponseModels;

namespace UserAccountManagement.Services;

public interface ITransactionService
{
    List<TransactionResponseModel> GetTransactionsByAccountId(Guid accountId);
}