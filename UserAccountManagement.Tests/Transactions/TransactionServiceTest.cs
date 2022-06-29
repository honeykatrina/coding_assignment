using UserAccountManagement.TransactionModule.Models.Entities;
using UserAccountManagement.TransactionModule.Models.Responses;
using UserAccountManagement.TransactionModule.Repositories;
using UserAccountManagement.TransactionModule.Services;

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
        var actualTransactions = transactionService.GetTransactions();

        actualTransactions.Success.Should().BeTrue();
        actualTransactions.Error.Should().BeNull();
        actualTransactions.Model.Should().HaveCount(2);
        actualTransactions.Model.Should().BeEquivalentTo(transactionsResponse);
    }

    private static List<TransactionResponseModel> GetTransactionsResponse()
    {
        return new List<TransactionResponseModel>()
        {
            new TransactionResponseModel()
            {
                AccountId = Guid.Parse("1b61a218-84f3-411a-8238-e9c3196fd387"),
                Amount = 200,
                CreationDate = DateTime.Parse("2021-09-15T00:00:00+02:00")
            },
            new TransactionResponseModel()
            {
                AccountId = Guid.Parse("1b61a218-84f3-411a-8238-e9c3196fd387"),
                Amount = 100,
                CreationDate = DateTime.Parse("2021-07-26T00:00:00+02:00")
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
                AccountId = Guid.Parse("1b61a218-84f3-411a-8238-e9c3196fd387"),
                Amount = 200,
                CreationDate = DateTime.Parse("2021-09-15T00:00:00+02:00")
            },
            new Transaction()
            {
                Id = Guid.NewGuid(),
                AccountId = Guid.Parse("1b61a218-84f3-411a-8238-e9c3196fd387"),
                Amount = 100,
                CreationDate = DateTime.Parse("2021-07-26T00:00:00+02:00")
            }
        };
    }
}
