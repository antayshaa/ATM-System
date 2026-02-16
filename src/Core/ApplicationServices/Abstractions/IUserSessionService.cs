using ATMSystem.Core.ApplicationServices.OperationResults;

namespace ATMSystem.Core.ApplicationServices.Abstractions;

public interface IUserSessionService
{
    Task<ResultTypeValue<Guid>> CreateUserSession(Guid accountId, string password);

    Task<ResultType> DeleteUserSession(Guid sessionId);
}