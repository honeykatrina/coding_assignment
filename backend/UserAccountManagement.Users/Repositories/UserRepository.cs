using System.Text.Json;
using UserAccountManagement.Users.Models.Entities;

namespace UserAccountManagement.Users.Repositories;

public class UserRepository : IUserRepository
{
    private const string Path = "Data\\Users.json";
    public User GetByCustomerId(int customerId)
    {
        var jsonString = File.ReadAllText(Path);
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users.FirstOrDefault(x => x.CustomerId == customerId);
    }

    public List<User> GetAll()
    {
        var jsonString = File.ReadAllText(Path);
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        return users;
    }

    public void Update(User user)
    {
        var jsonString = File.ReadAllText(Path);
        var users = JsonSerializer.Deserialize<List<User>>(jsonString);
        var userToUpdate = users.First(x => x.Id == user.Id);
        users.Remove(userToUpdate);
        users.Add(user);
        using var file = File.CreateText(Path);
        file.WriteLine(JsonSerializer.Serialize(users));
    }
}
