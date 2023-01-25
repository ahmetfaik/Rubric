using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Rubric.Common.Settings.MassTransit;
using Rubric.Consumer.Observers;
using Rubric.Infastructure.Loggers;

namespace Rubric.Consumer.Installers;

public static class MassTransitInstaller
{
     public static void InstallMassTransit(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var busSettings = new BusSettings(); 
        
        configuration.GetSection("BusSettings").Bind(busSettings);

        serviceCollection.AddMassTransit(x =>
        {
            // add consumer to bus

            // TODO:
            // x.addconsumer<>();

            // init bus
            x.UsingRabbitMq((context, cfg) =>
            {
                // observe pre - post - fault states of consumer
                cfg.ConnectConsumeObserver(new ConsumeObserver(new ConsoleLogger()));

                cfg.Host(new Uri($"{busSettings.ClusterAddress}"), a =>
                {
                    a.Username(busSettings.Username);
                    a.Password(busSettings.Password);
                });

                
                cfg.ReceiveEndpoint("QueueNames.QueueName", ep =>
                {
                    ep.AutoDelete = false;

                    ep.Durable = true;

                    ep.ExchangeType = ExchangeType.Fanout;

                    ep.UseMessageRetry(r => { r.Interval(3, TimeSpan.FromMilliseconds(1000)); });

                    ep.PrefetchCount = 10;

                    ep.UseKillSwitch(options => options
                        .SetActivationThreshold(10)
                        .SetTripThreshold(0.15)
                        .SetRestartTimeout(m: 1));

                    // TODO:
                    // ep.ConfigureConsumer<>(context);
                });
            });
        });
    }
}