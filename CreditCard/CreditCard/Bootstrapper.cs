using CreditCard.RabbitMQ.Consumer;
using CreditCard.RabbitMQ.Publisher;
using CreditCard.RabbitMQ.Settings;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

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

                        //Using Polly to establish a resilient connection
                        //var connection = GetRabbitMQConnectionPolicy().Execute(() => { return connectionFactory.CreateConnection(); });
                        //return connection;

                        return connectionFactory.CreateConnection();
                    });

            return services;
        }

        private static Policy GetRabbitMQConnectionPolicy() => Policy.Handle<SocketException>()
                                                                     .Or<BrokerUnreachableException>()
                                                                     .WaitAndRetry(retryCount: 5, _ => TimeSpan.FromSeconds(10),
                                                                     (exception, _, retryCount, context) => { Console.WriteLine($"Retry: {retryCount} | Exception: {exception.Message}"); });
    }
}
