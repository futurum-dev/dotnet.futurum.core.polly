using Futurum.Core.Result;

using Polly;

namespace Futurum.Core.Polly;

public static class ResultPolly
{
    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result.Result> TryAsync(Func<Task> func, Func<string> errorMessage, IAsyncPolicy policy)
    {
        try
        {
            await policy.ExecuteAsync(func);

            return Result.Result.Ok();
        }
        catch (Exception exception)
        {
            return Result.Result.Fail(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true with value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> TryAsync<T>(Func<Task<T>> func, Func<string> errorMessage, IAsyncPolicy policy)
    {
        try
        {
            var value = await policy.ExecuteAsync(func);

            return Result.Result.Ok(value);
        }
        catch (Exception exception)
        {
            return Result.Result.Fail<T>(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result.Result> TryAsync(Func<Task<Result.Result>> func, Func<string> errorMessage, IAsyncPolicy<Result.Result> policy)
    {
        try
        {
            var result = await policy.ExecuteAsync(func);

            return result.EnhanceWithError(errorMessage);
        }
        catch (Exception exception)
        {
            return Result.Result.Fail(exception.ToResultError(errorMessage()));
        }
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true with value.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static async Task<Result<T>> TryAsync<T>(Func<Task<Result<T>>> func, Func<string> errorMessage, IAsyncPolicy<Result<T>> policy)
    {
        try
        {
            var result = await policy.ExecuteAsync(func);

            return result.EnhanceWithError(errorMessage);
        }
        catch (Exception exception)
        {
            return Result.Result.Fail<T>(exception.ToResultError(errorMessage()));
        }
    }
}