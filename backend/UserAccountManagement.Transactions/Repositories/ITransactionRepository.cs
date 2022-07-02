using UserAccountManagement.Transactions.Models.Entities;

namespace UserAccountManagement.Transactions.Repositories;

public interface ITransactionRepository
{
    void Create(Transaction transaction);

    List<Transaction> GetAll();

    Transaction GetById(Guid id);
}