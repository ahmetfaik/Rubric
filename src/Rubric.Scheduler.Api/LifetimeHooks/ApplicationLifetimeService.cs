using Rubric.Infastructure.Loggers;

namespace Rubric.Scheduler.Api.LifetimeHooks;

public class ApplicationLifetimeService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IConsoleLogger _logger;

    public ApplicationLifetimeService(IHostApplicationLifetime applicationLifetime, IConsoleLogger logger)
    {
        _applicationLifetime = applicationLifetime;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _applicationLifetime.ApplicationStopping.Register(() =>
        {
            _logger.LogInformation("SIGTERM received, waiting for 30 seconds");
            Thread.Sleep(30_000);
            _logger.LogInformation("Termination delay complete, continuing stopping process");
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}