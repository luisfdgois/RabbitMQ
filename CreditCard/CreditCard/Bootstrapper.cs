using CreditCard.Bus.Consumer;
using CreditCard.Bus.Settings;
using MassTransit;

namespace CreditCard
{
    public static class Bootstrapper
    {
        public static IServiceCollection ConfigureConsumerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQProperties = configuration.GetSection("RabbitMQProperties").Get<RabbitMQProperties>();

            services.AddMassTransit(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();

                configure.AddConsumer<CreditCardConsumer>();
                         //.Endpoint(endpoint => {
                         //   endpoint.Name = "creditcard-queue";
                         //   //endpoint.PrefetchCount = 1;
                         //});

                configure.UsingRabbitMq((context, config) =>
                {
                    config.Host(new Uri(rabbitMQProperties!.Uri));
                    config.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
