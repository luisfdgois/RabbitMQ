using CreditCard;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services.AddHostedService<Worker>();

        services.ConfigureConsumerServices(configuration);
    })
    .Build();

await host.RunAsync();
