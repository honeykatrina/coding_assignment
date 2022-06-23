using Microsoft.AspNetCore.Mvc;
using UserAccountManagement.Services;

namespace UserAccountManagement.Controllers;

[ApiController]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("users/{accountId:guid}/transactions")]
    public IActionResult Get([FromRoute]Guid accountId)
    {
        return Ok(_transactionService.GetTransactionsByAccountId(accountId));
    }
}
