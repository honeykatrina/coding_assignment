using UserAccountManagement.TransactionModule.Models.Entities;

namespace UserAccountManagement.TransactionModule.Repositories;

public interface ITransactionRepository
{
    void Create(Transaction transaction);

    List<Transaction> GetAll();

    Transaction GetById(Guid id);
}