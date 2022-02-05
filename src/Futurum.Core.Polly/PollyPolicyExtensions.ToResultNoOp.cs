using Polly;

namespace Futurum.Core.Polly;

public static partial class PollyPolicyExtensions
{
    /// <summary>
    /// If <paramref name="policy"/> is null, then return a <see cref="Policy.NoOpAsync"/> <see cref="Result.Result"/>
    /// </summary>
    public static IAsyncPolicy<Result.Result> ToResultNoOp(this IAsyncPolicy<Result.Result>? policy) =>
        policy ?? Policy.NoOpAsync<Result.Result>();

    /// <summary>
    /// If <paramref name="policy"/> is null, then return a <see cref="Policy.NoOpAsync"/> <see cref="Result.Result{T}"/>
    /// </summary>
    public static IAsyncPolicy<Result.Result<T>> ToResultNoOp<T>(this IAsyncPolicy<Result.Result<T>>? policy) =>
        policy ?? Policy.NoOpAsync<Result.Result<T>>();
}