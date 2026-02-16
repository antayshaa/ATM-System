using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ATMSystem.Presentation.Controllers;

[Route("adminSession")]
public class AdminController : Controller
{
    private readonly IAdminSessionService _adminSessionService;

    public AdminController(IAdminSessionService adminSessionService)
    {
        _adminSessionService = adminSessionService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Login([FromBody] CreateAdminSession request)
    {
        ResultTypeValue<Guid> result = await _adminSessionService.CreateAdminSession(request.Password);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Result);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteAdminSession request)
    {
        ResultType result = await _adminSessionService.DeleteAdminSession(request.SessionId, request.Password);

        if (result.IsSuccess)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Result);
    }
}