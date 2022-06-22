namespace UserAccountManagement.Models;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public double Balance { get; set; }

    public Account[] Accounts { get; set; }
}
