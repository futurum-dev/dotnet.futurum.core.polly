using Futurum.Core.Result;

using Polly;

namespace Futurum.Core.Polly;

public static partial class ResultPolly
{
    /// <summary>
    /// Create a policy to handler <see cref="Result"/>
    /// </summary>
    public static PolicyBuilder<Result.Result> HandleResult() =>
        Policy.HandleResult<Result.Result>(x => x.IsFailure);

    /// <summary>
    /// Create a policy to handler <see cref="Result{T}"/>
    /// </summary>
    public static PolicyBuilder<Result<T>> HandleResult<T>() =>
        Policy.HandleResult<Result<T>>(x => x.IsFailure);
}