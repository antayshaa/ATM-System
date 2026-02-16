using ATMSystem.Core.ApplicationServices.OperationResults;

namespace ATMSystem.Core.ApplicationServices.Abstractions;

public interface IAdminSessionService
{
    Task<ResultTypeValue<Guid>> CreateAdminSession(string password);

    Task<ResultType> DeleteAdminSession(Guid sessionId, string password);
}