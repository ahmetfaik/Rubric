using MassTransit;
using Rubric.Common.Settings.MassTransit;

namespace Rubric.Scheduler.Api.Installers;

public static class MassTransitInstaller
{
    public static void InstallMassTransit(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var busSettings = new BusSettings();
        configuration.GetSection("BusSettings").Bind(busSettings);

        serviceCollection.AddMassTransit(x =>
        {
            // init bus
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"{busSettings.ClusterAddress}"), h =>
                {
                    h.Username(busSettings.Username);
                    h.Password(busSettings.Password);
                });
            });
        });
    }
}