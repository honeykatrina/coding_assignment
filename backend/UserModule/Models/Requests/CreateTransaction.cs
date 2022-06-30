using UserAccountManagement.Shared.Models;

namespace UserAccountManagement.UserModule.Models.Requests;

public class CreateTransaction: CustomMessage
{
    public Guid AccountId { get; set; }

    public double Amount { get; set; }
}
