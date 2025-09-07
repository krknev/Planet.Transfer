namespace Common.Lib;

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(bool isSuccess, T value, Error? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(Error error) => new(false, default!, error);

    public static implicit operator Result<T>(Error error) => Failure(error);
}

public class Result
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;

    public Error? Error { get; set; }
    protected Result(bool isSuccess, Error? error)
    {
        if (error is not null && isSuccess && error != Error.None
            || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }
    public static Result Accept() => new(true, null);
    public static Result Fail(Error error) => new(false, error);
}
public sealed record Error(string Codege, string Message)
{
    public static Error None { get; set; } = new(string.Empty, string.Empty);
    public static Error NotFound { get; set; } = new("NotFound", "The specified resource was not found.");
};
