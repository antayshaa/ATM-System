using ATMSystem.Core.ApplicationServices.OperationResults;

namespace ATMSystem.Core.ApplicationServices.Abstractions;

public interface IAuthenticationService
{
    Task<ResultType> AuthenticateAdmin(Guid sessionKey);

    Task<ResultTypeValue<Guid>> AuthenticateUser(Guid sessionKey);
}