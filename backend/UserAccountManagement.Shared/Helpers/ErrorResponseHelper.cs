using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserAccountManagement.Shared.Models;

namespace UserAccountManagement.Shared.Helpers;

public class ErrorResponseHelper
{
    public static IActionResult CreateErrorResponse(GeneralResponse response)
    {
        return response.Error.ErrorCode switch
        {
            HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
            HttpStatusCode.NotFound => new NotFoundObjectResult(response),
            _ => new BadRequestObjectResult(response),
        };
    }
}
