using UserAccountManagement.Transactions.Models.Entities;
using UserAccountManagement.Transactions.Models.Responses;
using UserAccountManagement.Transactions.Repositories;
using UserAccountManagement.Transactions.Services;

namespace UserAccountManagement.Tests.Transactions;

public class TransactionServiceTest
{
    [Fact]
    public void GetTransactionsShouldReturnListOfTransactions()
    {
        var transactions = GetTransactions();
        var transactionsResponse = GetTransactionsResponse();

        var transactionRepositoryMock = new Mock<ITransactionRepository>();
        transactionRepositoryMock.Setup(m => m.GetAll()).Returns(transactions);
        
        var mapperMock = new Mock<IMapper>();
        mapperMock
            .Setup(m => m.Map<List<TransactionResponseModel>>(transactions))
            .Returns(transactionsResponse);
        
        var transactionService = new TransactionService(
            transactionRepositoryMock.Object,
            mapperMock.Object);

        var actualResponse = transactionService.GetTransactions();

        actualResponse.Success.Should().BeTrue();
        actualResponse.Error.Should().BeNull();
        actualResponse.Model.Should().HaveCount(1);
        actualResponse.Model.Should().BeEquivalentTo(transactionsResponse);
    }

    private static List<TransactionResponseModel> GetTransactionsResponse()
    {
        return new List<TransactionResponseModel>()
        {
            new TransactionResponseModel()
            {
                AccountId = Guid.NewGuid(),
                Amount = 200,
                CreationDate = DateTime.Now
            }
        };
    }

    private static List<Transaction> GetTransactions()
    {
        return new List<Transaction>()
        {
            new Transaction()
            {
                Id = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Amount = 200,
                CreationDate = DateTime.Now
            }
        };
    }
}
