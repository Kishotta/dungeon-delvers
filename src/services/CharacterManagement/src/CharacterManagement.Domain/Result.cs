namespace CharacterManagement.Domain;

public class Result
{
    protected Result (bool isSuccess, string error)
    {
        switch (isSuccess)
        {
            case true when error != string.Empty:
                throw new InvalidOperationException();
            case false when error == string.Empty:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Error     = error;
                break;
        }
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    public static Result Success() => new Result(true, string.Empty);
    public static Result<TValue> Success<TValue> (TValue value) => new(value, true, string.Empty);

    public static Result Failure(string error) => new Result(false, error);
    public static Result<TValue> Failure<TValue> (string error) =>
        new(default!, false, error);

    public static Result<TValue> Create<TValue> (TValue? value, string error)
        where TValue : class =>
        value is null ? Failure<TValue> (error) : Success (value);
}

public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result (TValue value, bool isSuccess, string error)
        : base (isSuccess, error) =>
        _value = value;

    public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("The value of a failure result can not be accessed.");
}
