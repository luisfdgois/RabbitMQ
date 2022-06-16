using CreditCard.Service;
using Serilog;

void SetLogBehaviour(HostBuilderContext context, LoggerConfiguration configuration)
{
    configuration.ReadFrom.Configuration(context.Configuration)
                 .WriteTo.Console();
}

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services.AddHostedService<Worker>();

        services.ConfigureConsumerServices(configuration);
    })
    .UseSerilog(SetLogBehaviour)
    .Build();

await host.RunAsync();
