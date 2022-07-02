using System.Net;
using UserAccountManagement.Shared.ServiceBusServices;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Users.Models.Entities;
using UserAccountManagement.Users.Models.Responses;
using UserAccountManagement.Users.Repositories;
using UserAccountManagement.Users.Services;

namespace UserAccountManagement.Tests.Users;

public class UserServiceTest
{
    [Fact]
    public void GetUsersShouldReturnListOfUsers()
    {
        var users = GetUsers();
        var usersResponse = GetUsersResponse();

        var messageSenderMock = new Mock<IMessageSender>();
        var mapperMock = new Mock<IMapper>();
        var userRepositoryMock = new Mock<IUserRepository>();

        userRepositoryMock
            .Setup(m => m.GetAll())
            .Returns(users);
        mapperMock
            .Setup(m => m.Map<List<UserResponseModel>>(users))
            .Returns(usersResponse);

        var userService = new UserService(
            userRepositoryMock.Object,
            mapperMock.Object,
            messageSenderMock.Object);
        var actualResponse = userService.GetUsers();

        actualResponse.Success.Should().BeTrue();
        actualResponse.Error.Should().BeNull();
        actualResponse.Model.Should().HaveCount(1);
        actualResponse.Model.Should().BeEquivalentTo(usersResponse);
    }

    [Fact]
    public void GetUserAccountsByCustomerIdShouldReturnListOfAccounts()
    {
        var user = GetUser();
        var accountsResponse = GetAccountsResponse();

        var userRepositoryMock = new Mock<IUserRepository>();
        var messageSenderMock = new Mock<IMessageSender>();
        var mapperMock = new Mock<IMapper>();

        userRepositoryMock
            .Setup(m => m.GetByCustomerId(It.IsAny<int>()))
            .Returns(user);
        mapperMock
            .Setup(m => m.Map<List<AccountResponseModel>>(user.Accounts))
            .Returns(accountsResponse);

        var userService = new UserService(
            userRepositoryMock.Object,
            mapperMock.Object,
            messageSenderMock.Object);
        var actualResponse = userService.GetUserAccountsByCustomerId(1);

        actualResponse.Success.Should().BeTrue();
        actualResponse.Error.Should().BeNull();
        actualResponse.Model.Should().HaveCount(1);
        actualResponse.Model.Should().BeEquivalentTo(accountsResponse);
    }

    [Fact]
    public async Task GetUserAccountsByCustomerIdShouldReturnNewAccountAndCreateTransactionWhenItHasInitialCredit()
    {
        var request = GetRequest();
        var account = GetAccount();
        var accountResponse = GetAccountResponse();
        var user = GetUser();
        var transaction = GetCreateTransactionMessage();

        var messageSenderMock = new Mock<IMessageSender>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var mapperMock = new Mock<IMapper>();

        mapperMock
            .Setup(m => m.Map<Account>(request))
            .Returns(account);
        mapperMock
            .Setup(m => m.Map<AccountResponseModel>(account))
            .Returns(accountResponse);
        mapperMock
            .Setup(m => m.Map<CreateTransaction>(It.IsAny<(double, Guid)>()))
            .Returns(transaction);

        userRepositoryMock
            .Setup(m => m.GetByCustomerId(It.IsAny<int>()))
            .Returns(user);
        userRepositoryMock
            .Setup(m => m.Update(user))
            .Verifiable();

        messageSenderMock
            .Setup(m => m.SendMessageAsync(It.IsAny<string>()))
            .Verifiable();

        var userService = new UserService(
            userRepositoryMock.Object,
            mapperMock.Object,
            messageSenderMock.Object);
        var actualResponse = await userService.CreateUserAccountAsync(request);

        actualResponse.Success.Should().BeTrue();
        actualResponse.Error.Should().BeNull();
        actualResponse.Model.Should().BeEquivalentTo(accountResponse);
    }

    [Fact]
    public async Task GetUserAccountsByCustomerIdShouldReturnErrorWhenUserDoesNotExists()
    {
        var request = GetRequest();

        var mapperMock = new Mock<IMapper>();
        var messageSenderMock = new Mock<IMessageSender>();
        var userRepositoryMock = new Mock<IUserRepository>();

        userRepositoryMock
            .Setup(m => m.GetByCustomerId(It.IsAny<int>()))
            .Returns<User?>(null);

        var userService = new UserService(
            userRepositoryMock.Object,
            mapperMock.Object,
            messageSenderMock.Object);
        var actualResponse = await userService.CreateUserAccountAsync(request);

        actualResponse.Success.Should().BeFalse();
        actualResponse.Error.ErrorCode.Should().Be(HttpStatusCode.NotFound);
        actualResponse.Error.Message.Should().Be("Customer doesn't exist.");
    }

    private static User GetUser()
    {
        return new User()
        {
            Id = Guid.NewGuid(),
            CustomerId = 1,
            Name = string.Empty,
            Surname = string.Empty,
            Accounts = new()
            {
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Balance = 300,
                    CustomerId = 1
                }
            }
        };
    }

    private static List<User> GetUsers()
    {
        return new List<User>() { GetUser() };
    }

    private static CreateAccountRequest GetRequest()
    {
        return new CreateAccountRequest()
        {
            CustomerId = 1,
            InitialCredit = 100
        };
    }

    private static AccountResponseModel GetAccountResponse()
    {
        return new AccountResponseModel()
        {
            Id = Guid.NewGuid(),
            Balance=300
        };
    }

    private static List<AccountResponseModel> GetAccountsResponse()
    {
        return new List<AccountResponseModel>() { GetAccountResponse() };
    }

    private static CreateTransaction GetCreateTransactionMessage()
    {
        return new CreateTransaction()
        {
            AccountId = Guid.NewGuid(),
            Amount = 100
        };
    }

    private static Account GetAccount()
    {
        return new Account()
        {
            Id = Guid.NewGuid(),
            CustomerId = 1,
            Balance = 300
        };
    }

    private static List<UserResponseModel> GetUsersResponse()
    {
        return new List<UserResponseModel>()
            {
                new UserResponseModel()
                {
                    Name="Kate",
                    Surname="Morozova",
                    CustomerId=1,
                    Balance=300
                }
            };
    }
}
