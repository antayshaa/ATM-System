using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;

namespace ATMSystem.Infrastructure.InMemoryDataAccess;

public class InMemoryOperationRepository : IOperationRepository
{
    private readonly List<Operation> _operations = new();

    public Task AddOperation(Operation operation)
    {
        _operations.Add(operation);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Operation>> GetAccountOperationHistory(Guid accountId)
    {
        IReadOnlyCollection<Operation> history = _operations.Where(o => o.AccountId == accountId).ToList();
        return Task.FromResult(history);
    }

    public Task DeleteOperation(Guid operationId)
    {
        Operation? operation = _operations.FirstOrDefault(o => o.Id == operationId);
        if (operation != null)
        {
            _operations.Remove(operation);
        }

        return Task.CompletedTask;
    }

    public Task DeleteOperationsForAccount(Guid accountId)
    {
        _operations.RemoveAll(o => o.AccountId == accountId);
        return Task.CompletedTask;
    }
}