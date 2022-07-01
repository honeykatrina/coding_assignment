namespace UserAccountManagement.Users.Models.Entities;

public class User
{
    public Guid Id { get; set; }

    public int CustomerId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public List<Account> Accounts { get; set; }
}