using ATMSystem.Core.DomainModel;

namespace ATMSystem.Presentation.Models;

public class AccountHistoryResponse
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Date { get; set; } = string.Empty;

    public static AccountHistoryResponse CreateFrom(Operation operation)
    {
        return new AccountHistoryResponse
        {
            Id = operation.Id,
            AccountId = operation.AccountId,
            Type = operation.Type.ToString(),
            Date = operation.OccurredAt.ToString("dd.MM.yyyy HH:mm"),
        };
    }
}