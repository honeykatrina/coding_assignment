using System.Text.Json;
using UserAccountManagement.Models;

namespace UserAccountManagement.Repositories;

public interface IUserRepository
{
    List<User> GetUsers();
    void UpdateUser(User user);
}

public class UserRepository : IUserRepository
{
    public List<User> GetUsers()
    {
        var jsonString = File.ReadAllText("Users.json");
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users;
    }

    public void UpdateUser(User user)
    {
        var jsonString = File.ReadAllText("Users.json");
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        var existingUser = users.SingleOrDefault(x => x.Id == user.Id);
        users.Remove(existingUser);
        users.Add(user);
        using var file = File.CreateText("Users.json");
        file.WriteLine(JsonSerializer.Serialize(users));
    }
}
