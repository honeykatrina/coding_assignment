using System.Text.Json;
using UserAccountManagement.TransactionModule.Models.Entities;

namespace UserAccountManagement.TransactionModule.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private const string Path = "Data\\Transactions.json";

    public void Create(Transaction transaction)
    {
        var jsonString = File.ReadAllText(Path);
        var transactions = JsonSerializer.Deserialize<List<Transaction>>(jsonString);
        transactions.Add(transaction);
        using var file = File.CreateText(Path);
        file.WriteLine(JsonSerializer.Serialize(transactions));
    }

    public List<Transaction> GetByAccountId(Guid accountId)
    {
        var jsonString = File.ReadAllText(Path);
        var transactions = JsonSerializer.Deserialize<List<Transaction>>(jsonString);
        return transactions.Where(t => t.AccountId == accountId).ToList();
    }
}
