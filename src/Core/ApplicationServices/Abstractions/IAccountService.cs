using ATMSystem.Core.ApplicationServices.OperationResults;

namespace ATMSystem.Core.ApplicationServices.Abstractions;

public interface IAccountService
{
    Task<ResultTypeValue<Guid>> Create(Guid sessionKey, string password);

    Task<ResultType> Delete(Guid accountId);

    Task<ResultType> Deposit(Guid sessionKey, decimal amount);

    Task<ResultTypeValue<decimal>> GetBalance(Guid sessionKey);

    Task<ResultType> Withdraw(Guid sessionKey, decimal amount);
}