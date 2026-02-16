namespace ATMSystem.Infrastructure.DataBaseDataAccess.DTO;

public class AccountDbModel
{
    public Guid Id { get; init; }

    public decimal Balance { get; init; }

    public string Password { get; init; } = string.Empty;
}