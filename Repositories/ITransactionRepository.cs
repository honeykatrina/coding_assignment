using UserAccountManagement.Models.Entities;

namespace UserAccountManagement.Repositories;

public interface ITransactionRepository
{
    void Create(Transaction transaction);

    List<Transaction> GetByAccountId(Guid accountId);
}