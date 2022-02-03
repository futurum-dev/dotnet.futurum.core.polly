using Polly;

namespace Futurum.Core.Polly;

/// <summary>
/// Extension methods for Polly policy
/// </summary>
public static partial class PollyPolicyExtensions
{
    /// <summary>
    /// If <paramref name="policy"/> is null, then return a <see cref="Policy.NoOpAsync"/>
    /// </summary>
    public static IAsyncPolicy ToNoOp(this IAsyncPolicy? policy) =>
        policy ?? Policy.NoOpAsync();

    /// <summary>
    /// If <paramref name="policy"/> is null, then return a <see cref="Policy.NoOpAsync{T}"/>
    /// </summary>
    public static IAsyncPolicy<T> ToNoOp<T>(this IAsyncPolicy<T>? policy) =>
        policy ?? Policy.NoOpAsync<T>();
}