using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ATMSystem.Presentation.Controllers;

[Route("userSession")]
public class UserSessionController : Controller
{
    private readonly IUserSessionService _userSessionService;

    public UserSessionController(IUserSessionService userSessionService)
    {
        _userSessionService = userSessionService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Login([FromBody] CreateUserSession request)
    {
        ResultTypeValue<Guid> result = await _userSessionService.CreateUserSession(request.AccountId, request.Password);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Result);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteUserSession request)
    {
        ResultType result = await _userSessionService.DeleteUserSession(request.SessionId);

        if (result.IsSuccess)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Result);
    }
}