using UserAccountManagement.Models;
using UserAccountManagement.Repositories;

namespace UserAccountManagement.Services;

public interface IUserService
{
    List<User> GetUsers();
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<User> GetUsers()
    {
        return _userRepository.GetUsers();
    }
}

