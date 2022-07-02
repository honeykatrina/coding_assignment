using UserAccountManagement.Shared.Models;
using UserAccountManagement.Transactions.Models.Responses;

namespace UserAccountManagement.Transactions.Services;

public interface ITransactionService
{
    BaseResponse<List<TransactionResponseModel>> GetTransactions();
}