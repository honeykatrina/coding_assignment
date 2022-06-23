using UserAccountManagement.Models.Requests;
using UserAccountManagement.Models.ResponseModels;

namespace UserAccountManagement.Services;

public interface IUserService
{
    List<UserResponseModel> GetUsers();

    void CreateUser(CreateUserRequest request);
}