using Futurum.Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

namespace Futurum.Core.Polly;

public class PollyModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services.AddSingleton(PolicyStore.Registry);
    }
}