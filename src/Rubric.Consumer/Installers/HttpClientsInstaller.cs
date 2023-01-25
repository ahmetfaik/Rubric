using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rubric.Common.Settings.Statement;
using Rubric.Consumer.Clients.Handlers;

namespace Rubric.Consumer.Installers;

public static class HttpClientsInstaller
{
    public static void InstallHttpClients(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddTransient<RequestResponseLoggingHandler>();

        var statementSettings = new StatementSettings();
        
        configuration.GetSection("StatementSettings").Bind(statementSettings);
        
        // TODO: Add source clients
    }
}