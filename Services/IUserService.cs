using UserAccountManagement.Models.Requests;
using UserAccountManagement.Models.Responses;

namespace UserAccountManagement.Services;

public interface IUserService
{
    BaseResponse<List<UserResponseModel>> GetUsers();

    BaseResponse<UserResponseModel> CreateUser(CreateUserRequest request);
}