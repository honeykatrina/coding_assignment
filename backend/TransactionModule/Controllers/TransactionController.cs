using Microsoft.AspNetCore.Mvc;
using UserAccountManagement.Shared.Helpers;
using UserAccountManagement.TransactionModule.Services;

namespace UserAccountManagement.TransactionModule.Controllers;

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
        var result = _transactionService.GetTransactionsByAccountId(accountId);
        return result.Success ? new OkObjectResult(result) : ErrorResponseHelper.CreateErrorResponse(result);
    }
}
