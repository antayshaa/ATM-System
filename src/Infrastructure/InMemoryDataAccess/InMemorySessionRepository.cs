using ATMSystem.Core.DomainModel;
using ATMSystem.Core.Repositories;

namespace ATMSystem.Infrastructure.InMemoryDataAccess;

public class InMemorySessionRepository : ISessionRepository
{
    private readonly Dictionary<Guid, Session> _sessions = new();

    public Task AddSession(Session session)
    {
        _sessions[session.SessionKey] = session;
        return Task.CompletedTask;
    }

    public Task<Session?> FindSessionByKey(Guid sessionKey)
    {
        return Task.FromResult(_sessions.GetValueOrDefault(sessionKey));
    }

    public Task DeleteSession(Guid sessionKey)
    {
        _sessions.Remove(sessionKey);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Session>> GetSessionsByAccountId(Guid accountId)
    {
        IReadOnlyCollection<Session> sessions = _sessions.Values.Where(session => session.AccountId == accountId).ToList().AsReadOnly();
        return Task.FromResult(sessions);
    }
}