using Futurum.Core.Result;

using Polly;

namespace Futurum.Core.Polly;

/// <summary>
/// PolicyBuilder for <see cref="Result"/> and <see cref="Result{T}"/>
/// </summary>
public static partial class PollyPolicyBuilderExtensions
{
    /// <summary>
    /// Configures the policy to handler <see cref="Result"/>
    /// </summary>
    public static PolicyBuilder<Result.Result> HandleResult(this PolicyBuilder policyBuilder) =>
        policyBuilder.OrResult<Result.Result>(x => x.IsFailure);

    /// <summary>
    /// Configures the policy to handler <see cref="Result{T}"/>
    /// </summary>
    public static PolicyBuilder<Result<T>> HandleResult<T>(this PolicyBuilder policyBuilder) =>
        policyBuilder.OrResult<Result<T>>(x => x.IsFailure);
}