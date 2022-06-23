namespace UserAccountManagement.Models.DomainModels;

public class Account
{
    public Guid Id { get; set; }

    public int CustomerId { get; set; }
    
    public double Balance { get; set; }
}