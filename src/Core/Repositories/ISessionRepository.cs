using ATMSystem.Core.DomainModel;

namespace ATMSystem.Core.Repositories;

public interface ISessionRepository
{
    Task AddSession(Session session);

    Task<Session?> FindSessionByKey(Guid sessionKey);

    Task DeleteSession(Guid sessionKey);

    Task<IReadOnlyCollection<Session>> GetSessionsByAccountId(Guid accountId);
}