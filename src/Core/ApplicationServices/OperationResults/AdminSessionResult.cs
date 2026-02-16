namespace ATMSystem.Core.ApplicationServices.OperationResults;

public abstract class AdminSessionResult : ResultType
{
    protected AdminSessionResult(bool isSuccess, string? error = null)
        : base(isSuccess, error) { }

    public sealed class IncorrectPassword : AdminSessionResult
    {
        public IncorrectPassword() : base(false, "Incorrect admin password") { }
    }

    public sealed class IncorrectPassword<T> : ResultTypeValue<T>
    {
        public IncorrectPassword() : base(false, default, "Incorrect admin password") { }
    }

    public sealed class SessionNotExist : AdminSessionResult
    {
        public SessionNotExist() : base(false, "Session does not exist") { }
    }
}