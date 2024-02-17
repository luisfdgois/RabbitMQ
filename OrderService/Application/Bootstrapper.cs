using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(Bootstrapper).Assembly));

            return services;
        }
    }
}
