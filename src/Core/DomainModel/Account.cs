using ATMSystem.Core.ValueObjects;

namespace ATMSystem.Core.DomainModel;

public class Account
{
    public Guid Id { get; }

    public Money Balance { get; private set; } = new Money(0);

    public string Password { get; } = string.Empty;

    public Account() { }

    public Account(Guid id, decimal balance, string password)
    {
        Id = id;
        Balance = new Money(balance);
        Password = password;
    }

    public void WithdrawMoney(Money money)
    {
        Balance = new Money(Balance.Value - money.Value);
    }

    public void DepositMoney(Money money)
    {
        Balance = new Money(Balance.Value + money.Value);
    }
}