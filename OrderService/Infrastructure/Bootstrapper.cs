using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        private static string GetConnectionString(IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration["DBServer"]))
                return configuration.GetConnectionString("Postgres");

            return $"Host={configuration["DBServer"]};Port={configuration["DBPort"]};Database={configuration["DataBase"]};Username={configuration["DBUser"]};Password={configuration["DBPassword"]};";
        }
    }
}
