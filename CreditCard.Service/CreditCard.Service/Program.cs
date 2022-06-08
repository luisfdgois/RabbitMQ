using CreditCard.Service;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services.AddHostedService<Worker>();

        services.ConfigureConsumerServices(configuration);
    })
    .UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); })
    .Build();

await host.RunAsync();
