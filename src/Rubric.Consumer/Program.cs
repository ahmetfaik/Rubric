using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rubric.Common.Installers;
using Rubric.Consumer.Installers;

var hostBuilder = new HostBuilder()
    .ConfigureHostConfiguration(configHost =>
        configHost.AddEnvironmentVariables("ASPNETCORE_")
    )
    
#if DEBUG
    .UseEnvironment("Development")
#endif
    
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
        config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true);
        config.AddEnvironmentVariables();
        Console.WriteLine($"{hostingContext.HostingEnvironment.EnvironmentName}");
    })
    .ConfigureServices(ConfigureServices)
    .ConfigureLogging((hostBuilderContext, loggingBuilder) =>
    {
        loggingBuilder.AddConfiguration(hostBuilderContext.Configuration.GetSection("Logging"));
        loggingBuilder.AddConsole();
    });

await hostBuilder.RunConsoleAsync();

void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
{
    services.InstallLoggers(hostContext.Configuration);

    services.InstallSettings(hostContext.Configuration);

    services.InstallMassTransit(hostContext.Configuration);

    services.InstallRepositories(hostContext.Configuration);

    services.InstallHttpClients(hostContext.Configuration);

    ConfigureHostSettings(services);

    services.InstallHealthCheck();
    
    services.InstallMapper();

    services.InstallServices();
}

void ConfigureHostSettings(IServiceCollection services)
{
    services.Configure<HostOptions>(opts => opts.ShutdownTimeout = TimeSpan.FromSeconds(45));
}