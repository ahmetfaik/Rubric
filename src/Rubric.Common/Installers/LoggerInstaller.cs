using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rubric.Infastructure.Extensions;
using Rubric.Infastructure.Loggers;

namespace Rubric.Common.Installers;

public static class LoggerInstaller
{
    public static void InstallLoggers(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var value = configuration.GetSection("Logging:Console:LogLevel:Default").Value;
        
        if (value != null)
        {
            var defaultLogLevel = value.ToEnum(LogLevel.Error);
            Console.Out.WriteLine($"Console:LogLevel:Default: {defaultLogLevel}");
            ConsoleLogger.DefaultLogLevel = defaultLogLevel;
        }

        serviceCollection.AddSingleton<IConsoleLogger, ConsoleLogger>();
    }
}