using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.ApplicationServices.OperationResults;
using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;

namespace ATMSystem.Core.ApplicationServices.Services;

public class UserSessionService : IUserSessionService, IEventSubscriber
{
    private readonly IAccountRepository _accountRepository;

    private readonly ISessionRepository _sessionRepository;

    private readonly IPasswordService _passwordService;

    public UserSessionService(ISessionRepository sessionRepository, IAccountRepository accountRepository, IPasswordService passwordService)
    {
        _sessionRepository = sessionRepository;
        _accountRepository = accountRepository;
        _passwordService = passwordService;
    }

    public async Task<ResultTypeValue<Guid>> CreateUserSession(Guid accountId, string password)
    {
        Account? account = await _accountRepository.FindAccountById(accountId);
        if (account is null)
        {
            return new UserSessionResult.AccountNotExist<Guid>();
        }

        if (!_passwordService.VerifyPassword(password, account.Password))
        {
            return new UserSessionResult.IncorrectPassword<Guid>();
        }

        var sessionId = Guid.NewGuid();
        var session = new Session(sessionId, SessionType.User, accountId);
        await _sessionRepository.AddSession(session);
        return new ResultTypeValue<Guid>.SuccessResult(sessionId);
    }

    public async Task<ResultType> DeleteUserSession(Guid sessionId)
    {
        Session? session = await _sessionRepository.FindSessionByKey(sessionId);
        if (session is null)
        {
            return new UserSessionResult.SessionNotExist();
        }

        if (session.Type != SessionType.User)
        {
            return new UserSessionResult.NotEnoughRights();
        }

        await _sessionRepository.DeleteSession(sessionId);
        return new ResultType.Success();
    }

    public async Task Handle(Operation operation)
    {
        if (operation.Type != OperationType.AccountDeleted)
        {
            return;
        }

        IReadOnlyCollection<Session> sessions = await _sessionRepository.GetSessionsByAccountId(operation.AccountId);
        foreach (Session session in sessions)
        {
            await DeleteUserSession(session.SessionKey);
        }
    }
}