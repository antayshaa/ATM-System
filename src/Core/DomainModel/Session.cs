namespace ATMSystem.Core.DomainModel;

public class Session
{
    public Guid SessionKey { get; }

    public SessionType Type { get; }

    public Guid? AccountId { get; }

    public Session() { }

    public Session(Guid sessionKey, SessionType type, Guid? accountId)
    {
        SessionKey = sessionKey;
        Type = type;
        AccountId = accountId;
    }
}