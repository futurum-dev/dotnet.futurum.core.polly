using Futurum.Core.Option;

using Polly;
using Polly.Registry;

namespace Futurum.Core.Polly;

/// <summary>
/// Extension methods for PolicyRegistry
/// </summary>
public static class PolicyRegistryExtensions
{
    public static PolicyRegistry AddOrUpdate(this PolicyRegistry policyRegistry, string policyKey, IAsyncPolicy asyncPolicy)
    {
        IAsyncPolicy UpdatePolicyFactory(string _, IAsyncPolicy __) =>
            asyncPolicy;

        policyRegistry.AddOrUpdate(policyKey, asyncPolicy, UpdatePolicyFactory);

        return policyRegistry;
    }

    public static PolicyRegistry AddOrUpdate<T>(this PolicyRegistry policyRegistry, string policyKey, IAsyncPolicy<T> asyncPolicy)
    {
        IAsyncPolicy<T> UpdatePolicyFactory(string _, IAsyncPolicy<T> __) =>
            asyncPolicy;

        policyRegistry.AddOrUpdate(policyKey, asyncPolicy, UpdatePolicyFactory);

        return policyRegistry;
    }

    public static Option<T> TryGetPolicy<T>(this IReadOnlyPolicyRegistry<string> policyRegistry, string key)
        where T : IsPolicy =>
        policyRegistry.ContainsKey(key)
            ? policyRegistry[key] switch
            {
                T typedPolicy => typedPolicy.ToOption(),
                _             => Option<T>.None
            }
            : Option<T>.None;
}