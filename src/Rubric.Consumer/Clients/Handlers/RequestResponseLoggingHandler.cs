using System.Diagnostics;
using Rubric.Infastructure.Encryption;
using Rubric.Infastructure.Loggers;

namespace Rubric.Consumer.Clients.Handlers;

public class RequestResponseLoggingHandler : DelegatingHandler
{
    private readonly IConsoleLogger _logger;

    public RequestResponseLoggingHandler(IConsoleLogger logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        var requestBody = request.Content != null
            ? await request.Content.ReadAsStringAsync(cancellationToken)
            : null;

        requestBody = requestBody!.Encrypt();

        await _logger.LogInformation("Client request started", null, null,
            requestBody, request.Method, null, null, request.RequestUri!.Host, request.RequestUri.AbsolutePath);

        var response = await base.SendAsync(request, cancellationToken);

        stopwatch.Stop();

        var responseMessage =
            "Because of response sizes are too big, we don't log success scenarios.";

        if (!response.IsSuccessStatusCode)
            responseMessage = await response.Content.ReadAsStringAsync(cancellationToken);

        await _logger.LogInformation("Client request finished",
            null, responseMessage, requestBody, request.Method,
            response.StatusCode, stopwatch.ElapsedMilliseconds, request.RequestUri.Host,
            request.RequestUri.AbsolutePath);

        if (!response.IsSuccessStatusCode)
            try
            {
                var message =
                    $"Client Error - HTTP Status Code: {response.StatusCode}, URI: {request.RequestUri.AbsolutePath}, Message: {responseMessage}";

                await _logger.LogError(message);
            }
            catch
            {
                return response;
            }

        return response;
    }
}