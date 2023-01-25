using System.Text.Json;
using MassTransit;
using Rubric.Contracts.Common;
using Rubric.Infastructure.Loggers;

namespace Rubric.Consumer.Observers;

public class ConsumeObserver : IConsumeObserver
{
    private readonly IConsoleLogger _consumerLogger;

    public ConsumeObserver(IConsoleLogger consumerLogger)
    {
        _consumerLogger = consumerLogger;
    }

    public async Task PreConsume<T>(ConsumeContext<T> context) where T : class
    {
        var message = (IContract) context.Message;

        await _consumerLogger.LogInformation(
            $"Event:{JsonSerializer.Serialize(context.Message)} - Host:{context.Host}",
            correlationId: message.CorrelationId);
        await Task.CompletedTask;
    }

    public async Task PostConsume<T>(ConsumeContext<T> context) where T : class
    {
        await Task.CompletedTask;
    }

    public async Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
    {
        var message = (IContract) context.Message;
        await _consumerLogger.LogError(
            $"TEvent:{JsonSerializer.Serialize(context.Message)} - Host:{context.Host} - ReceiveEndpoint:{context.ReceiveContext}",
            exception, correlationId: message.CorrelationId);
    }
}