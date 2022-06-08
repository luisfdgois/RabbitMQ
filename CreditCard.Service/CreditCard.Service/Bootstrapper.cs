using CreditCard.Service.RabbitMQ.Consumer;
using CreditCard.Service.RabbitMQ.Publisher;
using RabbitMQ.Client;

namespace CreditCard.Service
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
                        var connectionFactory = new ConnectionFactory { Uri = new Uri(rabbitMQProperties.Uri) };
                        return connectionFactory.CreateConnection();
                    });

            return services;
        }
    }
}
