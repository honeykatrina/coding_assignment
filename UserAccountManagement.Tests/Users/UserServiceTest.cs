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
        var userService = new UserService(
            userRepositoryMock.Object,
            transactionRepositoryMock.Object,
            mapperMock.Object);
        var actualUsers = userService.GetUsers();

        actualUsers.Success.Should().BeTrue();
        actualUsers.Error.Should().BeNull();
        actualUsers.Model.Should().HaveCount(1);
        actualUsers.Model.Should().BeEquivalentTo(usersResponse);
    }

    private static List<UserResponseModel> GetUsersResponse()
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

    private static List<User> GetUsers()
    {
        return new List<User>()
        {
            new User()
            {
                Id = Guid.NewGuid(),
                Name="Kate",
                Surname="Morozova",
                Account= new Account {
                    Id= Guid.Parse("1b61a218-84f3-411a-8238-e9c3196fd387"),
                    CustomerId=1,
                    Balance=300
                }
            }
        };
    }
}
