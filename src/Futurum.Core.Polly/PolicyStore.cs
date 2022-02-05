using Polly.Registry;

namespace Futurum.Core.Polly;

/// <summary>
/// Single static instance of <see cref="PolicyRegistry"/>
/// </summary>
public static class PolicyStore
{
    public static readonly PolicyRegistry Registry = new();
}