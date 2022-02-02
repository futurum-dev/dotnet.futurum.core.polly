using Futurum.Core.Result;

using Polly;

namespace Futurum.Core.Polly;

/// <summary>
/// PolicyBuilder for <see cref="Core.Result.Result"/> and <see cref="Core.Result.Result{T}"/>
/// </summary>
public static class PollyPolicyBuilderExtensions
{
    /// <summary>
    /// Configures the policy to handler <see cref="Core.Result.Result"/>
    /// </summary>
    public static PolicyBuilder<Core.Result.Result> HandleResult(this PolicyBuilder policyBuilder) =>
        policyBuilder.OrResult<Core.Result.Result>(x => x.IsFailure);

    /// <summary>
    /// Configures the policy to handler <see cref="Core.Result.Result{T}"/>
    /// </summary>
    public static PolicyBuilder<Result<T>> HandleResult<T>(this PolicyBuilder policyBuilder) =>
        policyBuilder.OrResult<Result<T>>(x => x.IsFailure);
}