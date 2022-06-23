using System.Text.Json;
using UserAccountManagement.Models.DomainModels;

namespace UserAccountManagement.Repositories;

public interface IUserRepository
{
    List<User> GetAll();

    void Create(User user);

    User GetByCustomerId(int customerId);
}

public class UserRepository : IUserRepository
{
    public User GetByCustomerId(int customerId)
    {
        var jsonString = File.ReadAllText("Users.json");
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users.FirstOrDefault(x => x.Account.CustomerId == customerId);
    }

    public List<User> GetAll()
    {
        var jsonString = File.ReadAllText("Users.json");
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users;
    }

    public void Create(User user)
    {
        var jsonString = File.ReadAllText("Users.json");
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        users.Add(user);
        using var file = File.CreateText("Users.json");
        file.WriteLine(JsonSerializer.Serialize(users));
    }
}
