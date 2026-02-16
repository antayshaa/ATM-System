using ATMSystem.Core.DomainModel;

namespace ATMSystem.Core.Repositories;

public interface IAccountRepository
{
    Task<Account?> FindAccountById(Guid accountId);

    Task UpdateAccount(Account account);

    Task AddAccount(Account account);

    Task DeleteAccount(Guid accountId);
}