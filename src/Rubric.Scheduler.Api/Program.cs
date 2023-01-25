using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Rubric.Common.Installers;
using Rubric.Scheduler.Api.Installers;
using Rubric.Scheduler.Api.LifetimeHooks;

Console.WriteLine("Rubric Scheduler starting...");

var builder = WebApplication.CreateBuilder(args);

ConfigureHostSettings(builder.Host);

Console.WriteLine("Configured Host Settings...");

ConfigurationSettings(builder.Configuration);
RegisterServices(builder.Services, builder.Configuration);

Console.WriteLine("Services Registered...");

var app = builder.Build();

ConfigureWebApplication(app);

await app.RunAsync();

void ConfigureHostSettings(IHostBuilder hostBuilder)
{
    hostBuilder.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(45));
}

void ConfigurationSettings(IConfigurationBuilder configurationBuilder)
{
    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
    configurationBuilder.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
    configurationBuilder.AddEnvironmentVariables();
}

void RegisterServices(IServiceCollection serviceCollection, IConfiguration configurationRoot)
{
    //Settings
    serviceCollection.InstallSettings(configurationRoot);

    //Loggers
    serviceCollection.InstallLoggers(configurationRoot);

    //HealthChecks
    serviceCollection.AddHealthChecks();

    //Controllers
    serviceCollection.InstallController();

    //Hangfire
    serviceCollection.InstallHangfire(configurationRoot);

    //LifeTimeServices
    serviceCollection.AddHostedService<ApplicationLifetimeService>();

    //MassTransit
    serviceCollection.InstallMassTransit(configurationRoot);

    //Swagger
    serviceCollection.InstallSwagger();
}

void ConfigureWebApplication(IApplicationBuilder applicationBuilder)
{
    var provider = applicationBuilder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
    applicationBuilder.UseHttpsRedirection();
    applicationBuilder.UseRouting();
    applicationBuilder.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    applicationBuilder.ConfigureHangfire();
    applicationBuilder.InstallHealthCheck();
    applicationBuilder.UseSwagger();
    applicationBuilder.UseSwaggerUI(
        options =>
        {
            // build a swagger endpoint for each discovered API version
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
        });
}