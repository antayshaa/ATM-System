using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;
using ATMSystem.Core.ValueObjects;

namespace ATMSystem.Core.ApplicationServices.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    private readonly IAuthenticationService _authenticationService;

    private readonly IEventPublisher _publisher;

    private readonly IOperationRepository _operationRepository;

    private readonly IPasswordService _passwordService;

    public AccountService(IAuthenticationService authenticationService, IAccountRepository accountRepository, IEventPublisher publisher, IOperationRepository operationRepository, IPasswordService passwordService)
    {
        _accountRepository = accountRepository;
        _authenticationService = authenticationService;
        _publisher = publisher;
        _operationRepository = operationRepository;
        _passwordService = passwordService;
    }

    public async Task<ResultTypeValue<Guid>> Create(Guid sessionKey, string password)
    {
        ResultType result = await _authenticationService.AuthenticateAdmin(sessionKey);

        if (!result.IsSuccess)
        {
            return new AccountOperationResult.AuthenticateFaild<Guid>(result.Result);
        }

        var accountId = Guid.NewGuid();
        var newAccount = new Account(accountId, 0, _passwordService.GetHash(password));
        await _accountRepository.AddAccount(newAccount);
        await _publisher.Publish(new Operation(Guid.NewGuid(), newAccount.Id, OperationType.AccountCreated, DateTime.Now));

        return new ResultTypeValue<Guid>.SuccessResult(accountId);
    }

    public async Task<ResultType> Delete(Guid accountId)
    {
        Account? account = await _accountRepository.FindAccountById(accountId);

        if (account == null)
        {
            return new AccountOperationResult.AccountNotExist();
        }

        if (account.Balance.Value != 0)
        {
            return new AccountOperationResult.AccountHasMoney();
        }

        await _operationRepository.DeleteOperationsForAccount(account.Id);
        await _publisher.Publish(new Operation(Guid.NewGuid(), account.Id, OperationType.AccountDeleted, DateTime.Now));
        await _accountRepository.DeleteAccount(account.Id);

        return new ResultType.Success();
    }

    public async Task<ResultType> Deposit(Guid sessionKey, decimal amount)
    {
        ResultTypeValue<Guid> result = await _authenticationService.AuthenticateUser(sessionKey);

        if (!result.IsSuccess)
        {
            return new AccountOperationResult.AuthenticateFaild(result.Result);
        }

        Account? account = await _accountRepository.FindAccountById(result.Value);

        if (account == null)
        {
            return new AccountOperationResult.AccountNotExist();
        }

        try
        {
            var money = new Money(amount);
            account.DepositMoney(money);
        }
        catch (Exception)
        {
            return new AccountOperationResult.InValidMoneyValue();
        }

        await _accountRepository.UpdateAccount(account);
        await _publisher.Publish(new Operation(Guid.NewGuid(), account.Id, OperationType.Deposit, DateTime.Now));

        return new ResultType.Success();
    }

    public async Task<ResultTypeValue<decimal>> GetBalance(Guid sessionKey)
    {
        ResultTypeValue<Guid> result = await _authenticationService.AuthenticateUser(sessionKey);

        if (!result.IsSuccess)
        {
            return new AccountOperationResult.AuthenticateFaild<decimal>(result.Result);
        }

        Account? account = await _accountRepository.FindAccountById(result.Value);

        if (account == null)
        {
            return new AccountOperationResult.AccountNotExist<decimal>();
        }

        Money balance = account.Balance;
        await _publisher.Publish(new Operation(Guid.NewGuid(), account.Id, OperationType.GetBalance, DateTime.Now));

        return new ResultTypeValue<decimal>.SuccessResult(balance.Value);
    }

    public async Task<ResultType> Withdraw(Guid sessionKey, decimal amount)
    {
        ResultTypeValue<Guid> result = await _authenticationService.AuthenticateUser(sessionKey);

        if (!result.IsSuccess)
        {
            return new AccountOperationResult.AuthenticateFaild(result.Result);
        }

        Account? account = await _accountRepository.FindAccountById(result.Value);

        if (account == null)
        {
            return new AccountOperationResult.AccountNotExist();
        }

        try
        {
            var money = new Money(amount);
            if (account.Balance.Value < money.Value)
            {
                return new AccountOperationResult.InsufficientFunds();
            }

            account.WithdrawMoney(money);
        }
        catch (Exception)
        {
            return new AccountOperationResult.InValidMoneyValue();
        }

        await _accountRepository.UpdateAccount(account);
        await _publisher.Publish(new Operation(Guid.NewGuid(), account.Id, OperationType.Withdrawal, DateTime.Now));

        return new ResultType.Success();
    }
}