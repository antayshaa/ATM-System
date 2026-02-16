namespace ATMSystem.Core.ApplicationServices.OperationResults;

public abstract class UserSessionResult : ResultType
{
    protected UserSessionResult(bool isSuccess, string? error = null)
        : base(isSuccess, error) { }

    public sealed class AccountNotExist<T> : ResultTypeValue<T>
    {
        public AccountNotExist() : base(false, default, "Account does not exist") { }
    }

    public sealed class IncorrectPassword<T> : ResultTypeValue<T>
    {
        public IncorrectPassword() : base(false, default, "Incorrect admin password") { }
    }

    public sealed class SessionNotExist : UserSessionResult
    {
        public SessionNotExist() : base(false, "Session does not exist") { }
    }

    public sealed class NotEnoughRights : UserSessionResult
    {
        public NotEnoughRights() : base(false, "Incorrect password") { }
    }
}