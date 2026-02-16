using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Core.DomainModel;

namespace ATMSystem.Core.ApplicationServices.Abstractions;

public interface IHistoryService
{
    Task<ResultTypeValue<IReadOnlyCollection<Operation>>> GetHistory(Guid sessionKey);
}