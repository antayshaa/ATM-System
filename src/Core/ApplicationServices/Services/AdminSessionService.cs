using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;

namespace ATMSystem.Core.ApplicationServices.Services;

public class AdminSessionService : IAdminSessionService
{
    private readonly ISessionRepository _sessionRepository;

    private readonly ISystemPassword _systemPassword;

    public AdminSessionService(ISessionRepository sessionRepository, ISystemPassword systemPassword)
    {
        _sessionRepository = sessionRepository;
        _systemPassword = systemPassword;
    }

    public async Task<ResultTypeValue<Guid>> CreateAdminSession(string password)
    {
        if (!await _systemPassword.IsValidPassword(password))
        {
            return new AdminSessionResult.IncorrectPassword<Guid>();
        }

        var sessionId = Guid.NewGuid();
        var session = new Session(sessionId, SessionType.Admin, null);
        await _sessionRepository.AddSession(session);
        return new ResultTypeValue<Guid>.SuccessResult(sessionId);
    }

    public async Task<ResultType> DeleteAdminSession(Guid sessionId, string password)
    {
        if (!await _systemPassword.IsValidPassword(password))
        {
            return new AdminSessionResult.IncorrectPassword();
        }

        Session? session = await _sessionRepository.FindSessionByKey(sessionId);
        if (session is null)
        {
            return new AdminSessionResult.SessionNotExist();
        }

        await _sessionRepository.DeleteSession(sessionId);
        return new ResultType.Success();
    }
}