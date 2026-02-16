using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;

namespace ATMSystem.Core.ApplicationServices.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ISessionRepository _sessionRepository;

    public AuthenticationService(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<ResultType> AuthenticateAdmin(Guid sessionKey)
    {
        Session? session = await _sessionRepository.FindSessionByKey(sessionKey);

        if (session is null)
        {
            return new AuthenticateResult.SessionNotExist();
        }

        if (session.Type != SessionType.Admin)
        {
            return new AuthenticateResult.UserIsNotAdmin();
        }

        return new ResultType.Success();
    }

    public async Task<ResultTypeValue<Guid>> AuthenticateUser(Guid sessionKey)
    {
        Session? session = await _sessionRepository.FindSessionByKey(sessionKey);

        if (session == null)
        {
            return new AuthenticateResult.SessionNotExist<Guid>();
        }

        if (session.Type != SessionType.User)
        {
            return new AuthenticateResult.SessionHasNoAccount<Guid>();
        }

        if (session.AccountId == null)
        {
            return new AuthenticateResult.UserIsAdmin<Guid>();
        }

        return new ResultTypeValue<Guid>.SuccessResult(session.AccountId.Value);
    }
}