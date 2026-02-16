namespace ATMSystem.Core.ApplicationServices.OperationResults;

public abstract class HistoryOperationResult<T> : ResultTypeValue<T>
{
    protected HistoryOperationResult(bool isSuccess, T? value, string? error = null)
        : base(isSuccess, value, error) { }

    public sealed class AuthenticateFaild : HistoryOperationResult<T>
    {
        public AuthenticateFaild(string? message) : base(false, default, message) { }
    }
}