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
    private readonly IMessageSenderTypeFactory _messageSenderTypeFactory;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IMessageSenderTypeFactory messageSenderTypeFactory)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _messageSenderTypeFactory = messageSenderTypeFactory;
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
            await _messageSenderTypeFactory
                       .Create(MessageType.CreateTransaction)
                       .SendMessageAsync(transaction);
        }

        return new BaseResponseBuilder<UserResponseModel>()
            .BuildSuccessResponse(_mapper.Map<UserResponseModel>(createdUser));
    }
}