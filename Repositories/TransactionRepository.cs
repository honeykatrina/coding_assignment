using System.Text.Json;
using UserAccountManagement.Models.Entities;

namespace UserAccountManagement.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public void Create(Transaction transaction)
    {
        var jsonString = File.ReadAllText("Transactions.json");
        var transactions = JsonSerializer.Deserialize<List<Transaction>>(jsonString);
        transactions.Add(transaction);
        using var file = File.CreateText("Transactions.json");
        file.WriteLine(JsonSerializer.Serialize(transactions));
    }

    public List<Transaction> GetByAccountId(Guid accountId)
    {
        var jsonString = File.ReadAllText("Transactions.json");
        var transactions = JsonSerializer.Deserialize<List<Transaction>>(jsonString);
        return transactions.Where(t => t.AccountId == accountId).ToList();
    }
}
