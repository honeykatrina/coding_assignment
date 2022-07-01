using AutoMapper;
using System.Net;
using System.Text.Json;
using UserAccountManagement.Shared.Helpers;
using UserAccountManagement.Shared.Models;
using UserAccountManagement.Shared.ServiceBusServices;
using UserAccountManagement.UserModule.Models.Entities;
using UserAccountManagement.UserModule.Models.Responses;
using UserAccountManagement.UserModule.Repositories;

namespace UserAccountManagement.UserModule.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMessageSender _messageSender;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IMessageSender messageSender)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _messageSender = messageSender;
    }

    public BaseResponse<List<UserResponseModel>> GetUsers()
    {
        var users = _userRepository.GetAll();
        return new BaseResponseBuilder<List<UserResponseModel>>()
            .BuildSuccessResponse(_mapper.Map<List<UserResponseModel>>(users));
    }

    public async Task<BaseResponse<UserResponseModel>> CreateUserAsync(CreateUserRequest request)
    {
        var currentUser = _userRepository.GetByCustomerId(request.CustomerId);
        if (currentUser != null)
        {
            return new BaseResponseBuilder<UserResponseModel>()
                .SetError(new()
                    { ErrorCode = HttpStatusCode.BadRequest, Message = "User account already exists." })
                .Build();
        }

        var newUser = _mapper.Map<User>(request);
        _userRepository.Create(newUser);
        var createdUser = _userRepository.GetById(newUser.Id);
        
        if (request.InitialCredit != 0)
        {
            var transaction = JsonSerializer.Serialize(
                _mapper.Map<CreateTransaction>((request, newUser.Account.Id)));
            await _messageSender
                       .SendMessageAsync(transaction);
        }

        return new BaseResponseBuilder<UserResponseModel>()
            .BuildSuccessResponse(_mapper.Map<UserResponseModel>(createdUser));
    }
}