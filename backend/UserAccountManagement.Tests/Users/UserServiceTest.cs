using System.Net;
using UserAccountManagement.Shared.ServiceBusServices;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.TransactionModule.Repositories;
using UserAccountManagement.UserModule.Models.Entities;
using UserAccountManagement.UserModule.Models.Responses;
using UserAccountManagement.UserModule.Repositories;
using UserAccountManagement.UserModule.Services;

namespace UserAccountManagement.Tests.Users;

public class UserServiceTest
{
    [Fact]
    public void GetUsersShouldReturnListOfUsers()
    {
        var users = GetUsers();
        var usersResponse = GetUsersResponse();

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock
            .Setup(m => m.GetAll())
            .Returns(users);
        
        var transactionRepositoryMock = new Mock<ITransactionRepository>();
        var mapperMock = new Mock<IMapper>();
        mapperMock
            .Setup(m => m.Map<List<UserResponseModel>>(users))
            .Returns(usersResponse);
        var messageSenderMock = new Mock<IMessageSender>();
        var userService = new UserService(
            userRepositoryMock.Object,
            mapperMock.Object,
            messageSenderMock.Object);
        var actualResponse = userService.GetUsers();

        actualResponse.Success.Should().BeTrue();
        actualResponse.Error.Should().BeNull();
        actualResponse.Model.Should().HaveCount(1);
        actualResponse.Model.Should().BeEquivalentTo(usersResponse);

        static List<UserResponseModel> GetUsersResponse()
        {
            return new List<UserResponseModel>()
            {
                new UserResponseModel()
                {
                    Name="Kate",
                    Surname="Morozova",
                    AccountId= Guid.Parse("1b61a218-84f3-411a-8238-e9c3196fd387"),
                    CustomerId=1,
                    Balance=300
                }
            };
        }
    }

    [Fact]
    public async Task CreateUserShouldReturnNewUserAndCreateTransactionWhenUserHasInitialCredit()
    {
        var userRequest = GetCreateUserRequest();
        var user = GetUser();
        var userResponse = GetUserResponseModel();
        var transaction = GetCreateTransactionMessage(user);

        var mapperMock = new Mock<IMapper>();
        mapperMock
            .Setup(m => m.Map<User>(userRequest))
            .Returns(user);
        mapperMock
            .Setup(m => m.Map<UserResponseModel>(user))
            .Returns(userResponse);
        mapperMock
            .Setup(m => m.Map<CreateTransaction>(It.IsAny<(CreateUserRequest, Guid)>()))
            .Returns(transaction);

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock
            .Setup(m => m.GetByCustomerId(It.IsAny<int>()))
            .Returns<User?>(null);
        userRepositoryMock
            .Setup(m => m.Create(user))
            .Verifiable();
        userRepositoryMock
            .Setup(m => m.GetById(It.IsAny<Guid>()))
            .Returns(user);

        var messageSenderMock = new Mock<IMessageSender>();
        messageSenderMock
            .Setup(m => m.SendMessageAsync(It.IsAny<string>()))
            .Verifiable();
        var userService = new UserService(
            userRepositoryMock.Object,
            mapperMock.Object,
            messageSenderMock.Object);
        var actualResponse = await userService.CreateUserAsync(userRequest);

        actualResponse.Success.Should().BeTrue();
        actualResponse.Error.Should().BeNull();
        actualResponse.Model.Should().BeEquivalentTo(userResponse);

        static CreateTransaction GetCreateTransactionMessage(User user)
        {
            return new CreateTransaction()
            {
                AccountId = user.Account.Id,
                Amount = 100
            };
        }

        static User GetUser()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Surname = null,
                Account = new Account
                {
                    Id = Guid.NewGuid(),
                    CustomerId = 2,
                    Balance = 100
                }
            };
        }

        static CreateUserRequest GetCreateUserRequest()
        {
            return new CreateUserRequest()
            {
                Name = "Test",
                CustomerId = 2,
                InitialCredit = 100
            };
        }

        static UserResponseModel GetUserResponseModel()
        {
            return new UserResponseModel
            {
                Name = "Test",
                Surname = null,
                AccountId = Guid.NewGuid(),
                CustomerId = 2,
                Balance = 100
            };
        }
    }

    [Fact]
    public async Task CreateUserShouldReturnErrorWhenUserExists()
    {
        var userRequest = GetCreateUserRequest();
        var user = GetUsers().First(x => x.Account.CustomerId == userRequest.CustomerId);

        var mapperMock = new Mock<IMapper>();
        var transactionRepositoryMock = new Mock<ITransactionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock
            .Setup(m => m.GetByCustomerId(It.IsAny<int>()))
            .Returns(user);

        var messageSenderMock = new Mock<IMessageSender>();

        var userService = new UserService(
            userRepositoryMock.Object,
            mapperMock.Object,
            messageSenderMock.Object);
        var actualResponse = await userService.CreateUserAsync(userRequest);

        actualResponse.Success.Should().BeFalse();
        actualResponse.Error.ErrorCode.Should().Be(HttpStatusCode.BadRequest);
        actualResponse.Error.Message.Should().Be("User account already exists.");

        static CreateUserRequest GetCreateUserRequest()
        {
            return new CreateUserRequest()
            {
                Name = "Test",
                CustomerId = 1,
                InitialCredit = 100
            };
        }
    }

    private static List<User> GetUsers()
    {
        return new List<User>()
        {
            new User()
            {
                Id = Guid.NewGuid(),
                Name="Kate",
                Surname="Morozova",
                Account= new Account 
                {
                    Id= Guid.Parse("1b61a218-84f3-411a-8238-e9c3196fd387"),
                    CustomerId=1,
                    Balance=300
                }
            }
        };
    }
}
