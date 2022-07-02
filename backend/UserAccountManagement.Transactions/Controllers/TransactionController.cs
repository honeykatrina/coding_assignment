using Microsoft.AspNetCore.Mvc;
using UserAccountManagement.Shared.Helpers;
using UserAccountManagement.Transactions.Services;

namespace UserAccountManagement.Transactions.Controllers;

[ApiController]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("transactions")]
    public IActionResult Get()
    {
        var result = _transactionService.GetTransactions();

        return result.Success ? new OkObjectResult(result) : ErrorResponseHelper.CreateErrorResponse(result);
    }
}
