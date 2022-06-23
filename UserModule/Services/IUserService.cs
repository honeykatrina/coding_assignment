using UserAccountManagement.Shared.Models;
using UserAccountManagement.UserModule.Models.Requests;
using UserAccountManagement.UserModule.Models.Responses;

namespace UserAccountManagement.UserModule.Services;

public interface IUserService
{
    BaseResponse<List<UserResponseModel>> GetUsers();

    BaseResponse<UserResponseModel> CreateUser(CreateUserRequest request);
}