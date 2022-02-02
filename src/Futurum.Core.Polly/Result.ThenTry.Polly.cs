using Futurum.Core.Functional;
using Futurum.Core.Result;

using Polly;

namespace Futurum.Core.Polly;

public static class ResultPollyExtensions
{
    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Core.Result.Result> ThenTryAsync(this Core.Result.Result result, Func<Task> func, Func<string> errorMessage, IAsyncPolicy policy)
    {
        Task<Core.Result.Result> Execute() => Core.Result.Result.TryAsync(() => policy.ExecuteAsync(func), errorMessage);

        return result.ThenAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Core.Result.Result result, Func<Task<T>> func, Func<string> errorMessage, IAsyncPolicy<T> policy)
    {
        Task<Result<T>> Execute() => Core.Result.Result.TryAsync(() => policy.ExecuteAsync(func), errorMessage);

        return result.ThenAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="nextResult"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Core.Result.Result> ThenTryAsync(this Core.Result.Result result, Func<Task<Core.Result.Result>> nextResult, Func<string> errorMessage, IAsyncPolicy<Core.Result.Result> policy)
    {
        Task<Core.Result.Result> Success() => Core.Result.Result.TryAsync(() => policy.ExecuteAsync(nextResult), errorMessage);

        return result.ThenAsync(Success);
    }

    /// <summary>
    /// Try to run <paramref name="nextResult"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Core.Result.Result result, Func<Task<Result<T>>> nextResult, Func<string> errorMessage, IAsyncPolicy<Result<T>> policy)
    {
        Task<Result<T>> Success() => Core.Result.Result.TryAsync(() => policy.ExecuteAsync(nextResult), errorMessage);

        return result.ThenAsync(Success);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Result<T> result, Func<T, Task> func, Func<string> errorMessage, IAsyncPolicy policy)
    {
        Task<Result<T>> Execute(T value) => Core.Result.Result.TryAsync(() => policy.ExecuteAsync(() => func(value)), errorMessage)
                                                .MapAsync(() => value);

        return result.ThenAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Result<T> result, Func<T, Task<TR>> func, Func<string> errorMessage, IAsyncPolicy<TR> policy)
    {
        Task<Result<TR>> Execute(T value) => Core.Result.Result.TryAsync(() => policy.ExecuteAsync(() => func(value)), errorMessage);

        return result.ThenAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Result<T> result, Func<T, Task<Core.Result.Result>> func, Func<string> errorMessage, IAsyncPolicy<Core.Result.Result> policy)
    {
        Task<Result<T>> Success(T value) => Core.Result.Result.TryAsync(() => policy.ExecuteAsync(() => func(value)).MapAsync(() => value), errorMessage);

        return result.ThenAsync(Success);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Result<T> result, Func<T, Task<Result<TR>>> func, Func<string> errorMessage, IAsyncPolicy<Result<TR>> policy)
    {
        Task<Result<TR>> Success(T value) => Core.Result.Result.TryAsync(() => policy.ExecuteAsync(() => func(value)), errorMessage);

        return result.ThenAsync(Success);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Core.Result.Result> ThenTryAsync(this Task<Core.Result.Result> resultTask, Func<Task> func, Func<string> errorMessage, IAsyncPolicy policy)
    {
        Task<Core.Result.Result> Execute(Core.Result.Result result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Core.Result.Result> resultTask, Func<Task<T>> func, Func<string> errorMessage, IAsyncPolicy<T> policy)
    {
        Task<Result<T>> Execute(Core.Result.Result result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result<T>> resultTask, Func<T, Task> func, Func<string> errorMessage, IAsyncPolicy policy)
    {
        Task<Result<T>> Execute(Result<T> result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }


    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, Task<TR>> func, Func<string> errorMessage, IAsyncPolicy<TR> policy)
    {
        Task<Result<TR>> Execute(Result<T> result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Core.Result.Result> ThenTryAsync(this Task<Core.Result.Result> resultTask, Func<Task<Core.Result.Result>> func, Func<string> errorMessage,
                                                        IAsyncPolicy<Core.Result.Result> policy)
    {
        Task<Core.Result.Result> Execute(Core.Result.Result result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Core.Result.Result> resultTask, Func<Task<Result<T>>> func, Func<string> errorMessage, IAsyncPolicy<Result<T>> policy)
    {
        Task<Result<T>> Execute(Core.Result.Result result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result<T>> resultTask, Func<T, Task<Core.Result.Result>> func, Func<string> errorMessage, IAsyncPolicy<Core.Result.Result> policy)
    {
        Task<Result<T>> Execute(Result<T> result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Core.Result.Result"/> <see cref="Core.Result.Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, Task<Result<TR>>> func, Func<string> errorMessage, IAsyncPolicy<Result<TR>> policy)
    {
        Task<Result<TR>> Execute(Result<T> result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }
}