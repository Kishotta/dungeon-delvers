namespace DungeonDelvers.Common.Domain;

public static class ResultExtensions
{
    public static Result<TOut> Bind<TOut>(
        this Result result,
        Func<Result<TOut>> onSuccess) =>
        result.Match(
            onSuccess,
            _ => Result.Failure<TOut>(result.Error));
    
    public static Result<TValue> Tap<TValue>(
        this Result<TValue> result,
        Action<TValue> action) =>
        result.Match(
            value =>
            {
                action(value);
                return result;
            },
            _ => result);

    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure) =>
        result.IsSuccess ? onSuccess() : onFailure(result);

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure) =>
        result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
}