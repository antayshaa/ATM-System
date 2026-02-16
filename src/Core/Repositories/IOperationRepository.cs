using ATMSystem.Core.DomainModel;

namespace ATMSystem.Core.Repositories;

public interface IOperationRepository
{
    Task AddOperation(Operation operation);

    Task<IReadOnlyCollection<Operation>> GetAccountOperationHistory(Guid accountId);

    Task DeleteOperation(Guid operationId);

    Task DeleteOperationsForAccount(Guid accountId);
}