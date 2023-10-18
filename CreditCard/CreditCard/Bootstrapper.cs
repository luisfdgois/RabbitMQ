using CreditCard.Bus.Consumer;
using CreditCard.Bus.Publisher;
using CreditCard.Bus.Settings;
using RabbitMQ.Client;

namespace CreditCard
{
    public static class Bootstrapper
    {
        public static IServiceCollection ConfigureConsumerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQProperties = configuration.GetSection("RabbitMQProperties").Get<RabbitMQProperties>();

            services.AddTransient<IQueueConsumer, QueueConsumer>()
                    .AddTransient<IQueuePublisher, QueuePublisher>()
                    .AddTransient(serviceProvider =>
                    {
                        var connectionFactory = new ConnectionFactory { Uri = new Uri(rabbitMQProperties!.Uri) };
                        connectionFactory.ClientProvidedName = "app:CreditCard-Service";

                        return connectionFactory.CreateConnection();
                    });

            return services;
        }
    }
}
