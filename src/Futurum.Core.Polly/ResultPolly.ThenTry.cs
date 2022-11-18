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
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result.Result ThenTry(this Result.Result result, Action func, Func<string> errorMessage, ISyncPolicy policy)
    {
        Result.Result Execute() => Result.Result.Try(() => policy.Execute(func), errorMessage);

        return result.Then(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result.Result> ThenTryAsync(this Result.Result result, Func<Task> func, Func<string> errorMessage, IAsyncPolicy policy)
    {
        Task<Result.Result> Execute() => Result.Result.TryAsync(() => policy.ExecuteAsync(func), errorMessage);

        return result.ThenAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ThenTry<T>(this Result.Result result, Func<T> func, Func<string> errorMessage, ISyncPolicy<T> policy)
    {
        Result<T> Execute() => Result.Result.Try(() => policy.Execute(func), errorMessage);

        return result.Then(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Result.Result result, Func<Task<T>> func, Func<string> errorMessage, IAsyncPolicy<T> policy)
    {
        Task<Result<T>> Execute() => Result.Result.TryAsync(() => policy.ExecuteAsync(func), errorMessage);

        return result.ThenAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="nextResult"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result.Result ThenTry(this Result.Result result, Func<Result.Result> nextResult, Func<string> errorMessage, ISyncPolicy<Result.Result> policy)
    {
        Result.Result Success() => Result.Result.Try(() => policy.Execute(nextResult), errorMessage);

        return result.Then(Success);
    }

    /// <summary>
    /// Try to run <paramref name="nextResult"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result.Result> ThenTryAsync(this Result.Result result, Func<Task<Result.Result>> nextResult, Func<string> errorMessage, IAsyncPolicy<Result.Result> policy)
    {
        Task<Result.Result> Success() => Result.Result.TryAsync(() => policy.ExecuteAsync(nextResult), errorMessage);

        return result.ThenAsync(Success);
    }

    /// <summary>
    /// Try to run <paramref name="nextResult"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ThenTry<T>(this Result.Result result, Func<Result<T>> nextResult, Func<string> errorMessage, ISyncPolicy<Result<T>> policy)
    {
        Result<T> Success() => Result.Result.Try(() => policy.Execute(nextResult), errorMessage);

        return result.Then(Success);
    }

    /// <summary>
    /// Try to run <paramref name="nextResult"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="nextResult"/> <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Result.Result result, Func<Task<Result<T>>> nextResult, Func<string> errorMessage, IAsyncPolicy<Result<T>> policy)
    {
        Task<Result<T>> Success() => Result.Result.TryAsync(() => policy.ExecuteAsync(nextResult), errorMessage);

        return result.ThenAsync(Success);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ThenTry<T>(this Result<T> result, Action<T> func, Func<string> errorMessage, ISyncPolicy policy)
    {
        Result<T> Execute(T value) => Result.Result.Try(() => policy.Execute(() => func(value)), errorMessage)
                                                  .Map(() => value);

        return result.Then(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Result<T> result, Func<T, Task> func, Func<string> errorMessage, IAsyncPolicy policy)
    {
        Task<Result<T>> Execute(T value) => Result.Result.TryAsync(() => policy.ExecuteAsync(() => func(value)), errorMessage)
                                                  .MapAsync(() => value);

        return result.ThenAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TR> ThenTry<T, TR>(this Result<T> result, Func<T, TR> func, Func<string> errorMessage, ISyncPolicy<TR> policy)
    {
        Result<TR> Execute(T value) => Result.Result.Try(() => policy.Execute(() => func(value)), errorMessage);

        return result.Then(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Result<T> result, Func<T, Task<TR>> func, Func<string> errorMessage, IAsyncPolicy<TR> policy)
    {
        Task<Result<TR>> Execute(T value) => Result.Result.TryAsync(() => policy.ExecuteAsync(() => func(value)), errorMessage);

        return result.ThenAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<T> ThenTry<T>(this Result<T> result, Func<T, Result.Result> func, Func<string> errorMessage, ISyncPolicy<Result.Result> policy)
    {
        Result<T> Success(T value) => Result.Result.Try(() => policy.Execute(() => func(value)).Map(() => value), errorMessage);

        return result.Then(Success);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Result<T> result, Func<T, Task<Result.Result>> func, Func<string> errorMessage, IAsyncPolicy<Result.Result> policy)
    {
        Task<Result<T>> Success(T value) => Result.Result.TryAsync(() => policy.ExecuteAsync(() => func(value)).MapAsync(() => value), errorMessage);

        return result.ThenAsync(Success);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Result<TR> ThenTry<T, TR>(this Result<T> result, Func<T, Result<TR>> func, Func<string> errorMessage, ISyncPolicy<Result<TR>> policy)
    {
        Result<TR> Success(T value) => Result.Result.Try(() => policy.Execute(() => func(value)), errorMessage);

        return result.Then(Success);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="result"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Result<T> result, Func<T, Task<Result<TR>>> func, Func<string> errorMessage, IAsyncPolicy<Result<TR>> policy)
    {
        Task<Result<TR>> Success(T value) => Result.Result.TryAsync(() => policy.ExecuteAsync(() => func(value)), errorMessage);

        return result.ThenAsync(Success);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result.Result> ThenTryAsync(this Task<Result.Result> resultTask, Action func, Func<string> errorMessage, ISyncPolicy policy)
    {
        Result.Result Execute(Result.Result result) => result.ThenTry(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result.Result> ThenTryAsync(this Task<Result.Result> resultTask, Func<Task> func, Func<string> errorMessage, IAsyncPolicy policy)
    {
        Task<Result.Result> Execute(Result.Result result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result.Result> resultTask, Func<T> func, Func<string> errorMessage, ISyncPolicy<T> policy)
    {
        Result<T> Execute(Result.Result result) => result.ThenTry(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result.Result> resultTask, Func<Task<T>> func, Func<string> errorMessage, IAsyncPolicy<T> policy)
    {
        Task<Result<T>> Execute(Result.Result result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result<T>> resultTask, Action<T> func, Func<string> errorMessage, ISyncPolicy policy)
    {
        Result<T> Execute(Result<T> result) => result.ThenTry(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
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
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, TR> func, Func<string> errorMessage, ISyncPolicy<TR> policy)
    {
        Result<TR> Execute(Result<T> result) => result.ThenTry(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
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
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result.Result> ThenTryAsync(this Task<Result.Result> resultTask, Func<Result.Result> func, Func<string> errorMessage, ISyncPolicy<Result.Result> policy)
    {
        Result.Result Execute(Result.Result result) => result.ThenTry(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result.Result> ThenTryAsync(this Task<Result.Result> resultTask, Func<Task<Result.Result>> func, Func<string> errorMessage, IAsyncPolicy<Result.Result> policy)
    {
        Task<Result.Result> Execute(Result.Result result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result.Result> resultTask, Func<Result<T>> func, Func<string> errorMessage, ISyncPolicy<Result<T>> policy)
    {
        Result<T> Execute(Result.Result result) => result.ThenTry(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result.Result> resultTask, Func<Task<Result<T>>> func, Func<string> errorMessage, IAsyncPolicy<Result<T>> policy)
    {
        Task<Result<T>> Execute(Result.Result result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result<T>> resultTask, Func<T, Result.Result> func, Func<string> errorMessage, ISyncPolicy<Result.Result> policy)
    {
        Result<T> Execute(Result<T> result) => result.ThenTry(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<T>> ThenTryAsync<T>(this Task<Result<T>> resultTask, Func<T, Task<Result.Result>> func, Func<string> errorMessage, IAsyncPolicy<Result.Result> policy)
    {
        Task<Result<T>> Execute(Result<T> result) => result.ThenTryAsync(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }

    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    /// </list>
    /// </summary>
    public static Task<Result<TR>> ThenTryAsync<T, TR>(this Task<Result<T>> resultTask, Func<T, Result<TR>> func, Func<string> errorMessage, ISyncPolicy<Result<TR>> policy)
    {
        Result<TR> Execute(Result<T> result) => result.ThenTry(func, errorMessage, policy);

        return resultTask.PipeAsync(Execute);
    }
    
    /// <summary>
    /// Try to run <paramref name="func"/>, using the Polly policy.
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does not throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsSuccess"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/> <see cref="Result"/> <see cref="Result.IsSuccess"/> is true and <paramref name="func"/> does throw and exception, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         If <paramref name="resultTask"/>  <see cref="Result"/> <see cref="Result.IsFailure"/> is true, returns <see cref="Result{T}"/> with <see cref="Result{T}.IsFailure"/> true.
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