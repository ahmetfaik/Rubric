using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rubric.Persistence.PostgreSQL.Constants;

namespace Rubric.Persistence.PostgreSQL.Database;

public class DatabaseContext : DbContext
{
    private const string SchemaName = SchemaNames.DefaultSchemaName;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    // TODO: Dbset

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "",
                builder => builder.EnableRetryOnFailure(3)).EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}