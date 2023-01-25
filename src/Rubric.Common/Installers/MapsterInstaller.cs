using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Rubric.Common.Installers;

public static class MapsterInstaller
{
    public static void InstallMapster(this IServiceCollection serviceCollection)
    {
        var config = new TypeAdapterConfig();
        serviceCollection.AddSingleton(config);
    }
}