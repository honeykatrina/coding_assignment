namespace UserAccountManagement.Models;

public class Transaction
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public double Credit { get; set; }
}