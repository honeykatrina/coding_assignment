using System.Text.Json;
using UserAccountManagement.Models;

namespace UserAccountManagement.Repositories;

public interface IUserRepository
{
    List<User> GetUsers();
}

public class UserRepository : IUserRepository
{
   
    public List<User> GetUsers()
    {
        var jsonString = File.ReadAllText("Users.json");
        var users = JsonSerializer.Deserialize<User[]>(jsonString);
        return users.ToList();
    }
}
