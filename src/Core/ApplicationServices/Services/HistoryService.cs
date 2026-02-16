using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;

namespace ATMSystem.Core.ApplicationServices.Services;

public class HistoryService : IHistoryService, IEventSubscriber
{
    private readonly IAuthenticationService _authenticationService;

    private readonly IOperationRepository _operationRepository;

    public HistoryService(IAuthenticationService authenticationService, IOperationRepository operationRepository)
    {
        _authenticationService = authenticationService;
        _operationRepository = operationRepository;
    }

    public async Task Handle(Operation operation)
    {
        await _operationRepository.AddOperation(operation);
    }

    public async Task<ResultTypeValue<IReadOnlyCollection<Operation>>> GetHistory(Guid sessionKey)
    {
        ResultTypeValue<Guid> result = await _authenticationService.AuthenticateUser(sessionKey);

        if (!result.IsSuccess)
        {
            return new HistoryOperationResult<IReadOnlyCollection<Operation>>.AuthenticateFaild(result.Result);
        }

        IReadOnlyCollection<Operation> history = await _operationRepository.GetAccountOperationHistory(result.Value);
        return new ResultTypeValue<IReadOnlyCollection<Operation>>.SuccessResult(history);
    }
}