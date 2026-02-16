using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Core.ApplicationServices.Services;
using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;
using NSubstitute;
using Xunit;

namespace ATMSystem.Tests;

public class AtmSystemCoreTests
{
    private readonly IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();

    private readonly ISessionRepository _sessionRepository = Substitute.For<ISessionRepository>();

    private readonly IOperationRepository _operationRepository = Substitute.For<IOperationRepository>();

    private readonly IEventPublisher _eventPublisher = Substitute.For<IEventPublisher>();

    private readonly IPasswordService _passwordService = Substitute.For<IPasswordService>();

    [Fact]
    public async Task Withdraw_UpdatingBalanceCorrectly_SuccessfulAccountUpdateWithNewBalance()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        string pinCode = "1122";
        var authenticationService = new AuthenticationService(_sessionRepository);
        var session = new Session(sessionId, SessionType.User, accountId);
        var account = new Account(accountId, 1000, pinCode);
        var accountService = new AccountService(authenticationService, _accountRepository, _eventPublisher, _operationRepository, _passwordService);
        _sessionRepository.FindSessionByKey(sessionId).Returns(session);
        _accountRepository.FindAccountById(accountId).Returns(account);

        // Act
        ResultType result = await accountService.Withdraw(sessionId, 100);

        // Assert
        Assert.IsType<ResultType.Success>(result);
        await _accountRepository.Received(1).UpdateAccount(Arg.Is<Account>(a => a.Id == accountId && a.Balance.Value == 900));
    }

    [Fact]
    public async Task Withdraw_IncorrectBalanceUpdate_InsufficientFundsError()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        string pinCode = "1122";
        var authenticationService = new AuthenticationService(_sessionRepository);
        var session = new Session(sessionId, SessionType.User, accountId);
        var account = new Account(accountId, 0, pinCode);
        var accountService = new AccountService(authenticationService, _accountRepository, _eventPublisher, _operationRepository, _passwordService);
        _sessionRepository.FindSessionByKey(sessionId).Returns(session);
        _accountRepository.FindAccountById(accountId).Returns(account);

        // Act
        ResultType result = await accountService.Withdraw(sessionId, 100);

        // Assert
        Assert.IsType<AccountOperationResult.InsufficientFunds>(result);
    }

    [Fact]
    public async Task Deposit_UpdatingBalanceCorrectly_SuccessfulAccountUpdateWithNewBalance()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        string pinCode = "1122";
        var authenticationService = new AuthenticationService(_sessionRepository);
        var session = new Session(sessionId, SessionType.User, accountId);
        var account = new Account(accountId, 0, pinCode);
        var accountService = new AccountService(authenticationService, _accountRepository, _eventPublisher, _operationRepository, _passwordService);
        _sessionRepository.FindSessionByKey(sessionId).Returns(session);
        _accountRepository.FindAccountById(accountId).Returns(account);

        // Act
        ResultType result = await accountService.Deposit(sessionId, 100);

        // Assert
        Assert.IsType<ResultType.Success>(result);
        await _accountRepository.Received(1).UpdateAccount(Arg.Is<Account>(a => a.Id == accountId && a.Balance.Value == 100));
    }
}