using UserAccountManagement.Shared.Models;
using UserAccountManagement.TransactionModule.Models.Responses;

namespace UserAccountManagement.TransactionModule.Services;

public interface ITransactionService
{
    BaseResponse<List<TransactionResponseModel>> GetTransactions();
}