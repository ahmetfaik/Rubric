using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rubric.Common.Settings.Postgres;
using Rubric.Persistence.PostgreSQL.Database;

namespace Rubric.Consumer.Installers;

public static class RepositoryInstaller
{
    public static void InstallRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var postgresSettings = new PostgresSettings();
        configuration.GetSection("PostgresSettings").Bind(postgresSettings);

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseNpgsql(postgresSettings.ConnectionString);

        // TODO:
        /*serviceCollection.AddTransient<IDailyStatementReportRepository, DailyStatementReportRepository>(s =>
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ReportDatabaseContext>();
            dbContextOptionsBuilder.UseNpgsql(postgresSettings.ConnectionString);
            return new DailyStatementReportRepository(new ReportDatabaseContext(dbContextOptionsBuilder.Options));
        });*/
    }
}