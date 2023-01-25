using Hangfire;
using Hangfire.PostgreSql;
using HangfireBasicAuthenticationFilter;
using Rubric.Common.Settings.Hangfire;
using Rubric.Common.Settings.Postgres;

namespace Rubric.Scheduler.Api.Installers;

public static class HangfireInstaller
{
    public static void InstallHangfire(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var postgresSettings = new PostgresSettings();
        configuration.GetSection("PostgresSettings").Bind(postgresSettings);
        serviceCollection.AddHangfire(c =>
            c.UsePostgreSqlStorage(postgresSettings.ConnectionString, new PostgreSqlStorageOptions
            {
                DistributedLockTimeout = TimeSpan.FromMinutes(10),
                PrepareSchemaIfNecessary = true
            }).WithJobExpirationTimeout(TimeSpan.FromHours(6)));
        serviceCollection.AddHangfireServer(options => { options.ServerName = $"{Environment.MachineName}"; });
    }
    
     public static void ConfigureHangfire(this IApplicationBuilder applicationBuilder)
    {
        var hangfireDashboardSettings =
            applicationBuilder.ApplicationServices.GetRequiredService<HangfireSettings>();

        ArgumentNullException.ThrowIfNull(hangfireDashboardSettings, nameof(hangfireDashboardSettings));

        var jobManager = applicationBuilder.ApplicationServices.GetRequiredService<IRecurringJobManager>();

        applicationBuilder.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            AppPath = null,
            DashboardTitle = "Hangfire Dashboard",
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = hangfireDashboardSettings.Username,
                    Pass = hangfireDashboardSettings.Password
                }
            }
        });
    }
}