using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Rubric.Consumer.HealtChecks;

public class ConsumerHealthCheck : IHealthCheckPublisher
{
    private readonly string _fileName;
    private HealthStatus _prevStatus = HealthStatus.Unhealthy;

    public ConsumerHealthCheck()
    {
        _fileName = Environment.GetEnvironmentVariable("DOCKER_HEALTHCHECK_FILEPATH") ??
                    Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
    }

    public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        var fileExists = _prevStatus == HealthStatus.Healthy;

        if (report.Status == HealthStatus.Healthy)
        {
            using var _ = File.Create(_fileName);
        }
        else if (fileExists)
        {
            File.Delete(_fileName);
        }

        _prevStatus = report.Status;

        return Task.CompletedTask;
    }
}