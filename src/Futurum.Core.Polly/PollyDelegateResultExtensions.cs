using Futurum.Core.Result;

using Polly;

namespace Futurum.Core.Polly;

/// <summary>
/// Extension methods for DelegateResult
/// </summary>
public static partial class PollyDelegateResultExtensions
{
    /// <summary>
    /// Get the error message either from the <see cref="Exception"/> or the <see cref="Result"/>
    /// </summary>
    public static string GetErrorMessage(this DelegateResult<Result.Result> delegateResult) =>
        delegateResult.Exception != null
            ? delegateResult.Exception.ToString()
            : delegateResult.Result.Switch(() => string.Empty, error => error.ToErrorString());

    /// <summary>
    /// Get the error message either from the <see cref="Exception"/> or the <see cref="Result{T}"/>
    /// </summary>
    public static string GetErrorMessage<T>(this DelegateResult<Result.Result<T>> delegateResult) =>
        delegateResult.Exception != null
            ? delegateResult.Exception.ToString()
            : delegateResult.Result.Switch(_ => string.Empty, error => error.ToErrorString());

    /// <summary>
    /// Get the safe error message either from the <see cref="Exception"/> or the <see cref="Result"/>. Sensitive information (e.g. StackTraces) are not included
    /// </summary>
    public static string GetErrorMessageSafe(this DelegateResult<Result.Result> delegateResult) =>
        delegateResult.Exception != null
            ? delegateResult.Exception.Message
            : delegateResult.Result.Switch(() => string.Empty, error => error.ToErrorStringSafe());

    /// <summary>
    /// Get the safe error message either from the <see cref="Exception"/> or the <see cref="Result{T}"/>. Sensitive information (e.g. StackTraces) are not included
    /// </summary>
    public static string GetErrorMessageSafe<T>(this DelegateResult<Result.Result<T>> delegateResult) =>
        delegateResult.Exception != null
            ? delegateResult.Exception.Message
            : delegateResult.Result.Switch(_ => string.Empty, error => error.ToErrorStringSafe());
}