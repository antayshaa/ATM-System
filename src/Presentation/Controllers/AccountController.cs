using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Core.DomainModel;
using ATMSystem.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace ATMSystem.Presentation.Controllers;

[Route("account")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    private readonly IHistoryService _historyService;

    public AccountController(IAccountService accountService, IHistoryService historyService)
    {
        _accountService = accountService;
        _historyService = historyService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccount request)
    {
        ResultTypeValue<Guid> result = await _accountService.Create(request.SessionId, request.Password);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Result);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccount request)
    {
        ResultType result = await _accountService.Delete(request.AccountId);

        if (result.IsSuccess)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Result);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> DepositMoney([FromBody] MonetaryTransactions request)
    {
        ResultType result = await _accountService.Deposit(request.SessionId, request.Amount);

        if (result.IsSuccess)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Result);
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalance([FromQuery] Guid sessionId)
    {
        ResultTypeValue<decimal> result = await _accountService.GetBalance(sessionId);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Result);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawMoney([FromBody] MonetaryTransactions request)
    {
        ResultType result = await _accountService.Withdraw(request.SessionId, request.Amount);

        if (result.IsSuccess)
        {
            return Ok(result.Result);
        }

        return BadRequest(result.Result);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetOperationHistory([FromQuery] Guid sessionId)
    {
        ResultTypeValue<IReadOnlyCollection<Operation>> result = await _historyService.GetHistory(sessionId);

        if (result.IsSuccess)
        {
            var response = result.Value?.Select(AccountHistoryResponse.CreateFrom).ToList();
            return Ok(response);
        }

        return BadRequest(result.Result);
    }
}
