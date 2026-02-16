using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;

namespace ATMSystem.Infrastructure.InMemoryDataAccess;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly Dictionary<Guid, Account> _accounts = new();

    public Task<Account?> FindAccountById(Guid accountId)
    {
        return Task.FromResult(_accounts.GetValueOrDefault(accountId));
    }

    public Task UpdateAccount(Account account)
    {
        _accounts[account.Id] = account;
        return Task.CompletedTask;
    }

    public Task AddAccount(Account account)
    {
        _accounts[account.Id] = account;
        return Task.CompletedTask;
    }

    public Task DeleteAccount(Guid accountId)
    {
        _accounts.Remove(accountId);
        return Task.CompletedTask;
    }
}