using Domain.Services.Bus;
using Infrastructure.Data;
using Infrastructure.External.RabbitMQ.Contracts;
using Infrastructure.External.RabbitMQ.Implementations;
using Infrastructure.Services;
using Infrastructure.Services.Settings;
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
            services.AddSingleton<IPublisherServiceBus, PublisherServiceBus>();

            var rabbitMQProperties = configuration.GetSection("RabbitMQProperties").Get<RabbitMQProperties>();

            services.AddSingleton(serviceProvider => { return new ConnectionFactory { Uri = new Uri(rabbitMQProperties.Uri) }; })
                    .AddSingleton<IQueue, CreditCardQueue>();

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
