namespace UserAccountManagement.Models;

public class Account
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Transaction Transaction { get; set; }
}