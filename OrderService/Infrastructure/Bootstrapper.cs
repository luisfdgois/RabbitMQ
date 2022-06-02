using Domain.Services.Bus;
using Infrastructure.Data;
using Infrastructure.External.RabbitMQ.Publishers.Concrete;
using Infrastructure.External.RabbitMQ.Publishers.Contracts;
using Infrastructure.External.RabbitMQ.Shared.Settings;
using Infrastructure.Services;
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

        public static IServiceCollection AddInfrestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQProperties = configuration.GetSection("RabbitMQProperties").Get<RabbitMQProperties>();

            services.AddSingleton(serviceProvider =>
            {
                var connectionFactory = new ConnectionFactory { Uri = new Uri(rabbitMQProperties.Uri) };
                return connectionFactory.CreateConnection();
            });

            services.AddScoped<IPublisherServiceBus, PublisherServiceBus>()
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
