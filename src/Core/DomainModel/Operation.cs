namespace ATMSystem.Core.DomainModel;

public record Operation(Guid Id, Guid AccountId, OperationType Type, DateTime OccurredAt);