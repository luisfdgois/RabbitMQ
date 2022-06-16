using Domain.Repositories;
using Domain.Services.Bus;
using Infrastructure.Background;
using Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Concrete;
using Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Contracts;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Infrastructure.Shared.Settings;
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

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

        public static IServiceCollection AddInfrestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQProperties = configuration.GetSection("RabbitMQProperties").Get<RabbitMQProperties>();

            services.AddSingleton(serviceProvider =>
            {
                var connectionFactory = new ConnectionFactory { Uri = new Uri(rabbitMQProperties.Uri) };
                return connectionFactory.CreateConnection();
            });

            services.AddScoped<IPublisherServiceBus, PublisherServiceBus>()
                    .AddSingleton<IConsumerServiceBus, ConsumerServiceBus>()
                    .AddScoped<IPublisherQueue, CreditCardQueue>();

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
