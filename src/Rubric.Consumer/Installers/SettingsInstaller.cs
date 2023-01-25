using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Rubric.Common.Settings.Hangfire;
using Rubric.Common.Settings.MassTransit;
using Rubric.Common.Settings.Postgres;
using Rubric.Common.Settings.Validations;

namespace Rubric.Consumer.Installers;

public static class SettingsInstaller
{
    public static void InstallSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        //Options
        serviceCollection.AddOptions<PostgresSettings>().ValidateOnStart();
        serviceCollection.AddOptions<BusSettings>().ValidateOnStart();
        serviceCollection.AddOptions<HangfireSettings>().ValidateOnStart();

        //Options Configure
        serviceCollection.Configure<BusSettings>(configuration.GetSection("BusSettings"));
        var busSettings = configuration.Get<BusSettings>();
        configuration.GetSection(nameof(BusSettings)).Bind(busSettings);
        serviceCollection.AddSingleton(busSettings);

        serviceCollection.Configure<PostgresSettings>(configuration.GetSection("PostgresSettings"));
        var postgresSettings = configuration.Get<PostgresSettings>();
        configuration.GetSection(nameof(PostgresSettings)).Bind(postgresSettings);
        serviceCollection.AddSingleton(postgresSettings);

        serviceCollection.Configure<HangfireSettings>(configuration.GetSection("HangfireDashboardSettings"));
        var hangfireDashboardSettings = configuration.Get<HangfireSettings>();
        configuration.GetSection(nameof(HangfireSettings)).Bind(hangfireDashboardSettings);
        serviceCollection.AddSingleton(hangfireDashboardSettings);

        //Options Validators
        serviceCollection.AddSingleton<IValidateOptions<BusSettings>, MassTransitSettingsValidation>();
        serviceCollection.AddSingleton<IValidateOptions<PostgresSettings>, PostgresSettingsValidation>();
        serviceCollection.AddSingleton<IValidateOptions<HangfireSettings>, HangfireSettingsValidation>();
        
        // TODO: Configure
    }
}