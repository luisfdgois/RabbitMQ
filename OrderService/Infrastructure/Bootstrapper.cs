using Domain.Services.Bus;
using Infrastructure.Background;
using Infrastructure.Data;
using Infrastructure.Services.Bus.Consumers;
using Infrastructure.Services.Bus.Publishers;
using Infrastructure.Services.Bus.Publishers.Strategies;
using Infrastructure.Services.Bus.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddOrderContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = GetConnectionString(configuration);

            services.AddDbContext<OrderContext>(options => options.UseNpgsql(connectionString));

            return services;
        }

        public static IServiceCollection AddBackgroundService(this IServiceCollection services)
        {
            services.AddHostedService<PaymentProcessedWorker>();

            return services;
        }

        public static IServiceCollection AddInfrestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQProperties = configuration.GetSection("RabbitMQProperties").Get<RabbitMQProperties>();

            services.AddSingleton(_ =>
            {
                var connectionFactory = new ConnectionFactory { Uri = new Uri(rabbitMQProperties.Uri) };
                return connectionFactory.CreateConnection();
            });

            services.AddSingleton<IPublisherBus, PublisherBus>()
                    .AddSingleton<IStrategyPublisherBus, CreditCardBus>();

            services.AddSingleton<IConsumerBus, ConsumerBus>();

            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration["DBServer"]))
                return configuration.GetConnectionString("Postgres");

            return $"Host={configuration["DBServer"]};Port={configuration["DBPort"]};Database={configuration["DataBase"]};Username={configuration["DBUser"]};Password={configuration["DBPassword"]};";
        }
    }
}
