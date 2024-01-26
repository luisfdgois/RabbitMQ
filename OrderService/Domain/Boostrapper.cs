using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class Boostrapper
    {
        public static IServiceCollection AddDomainProcess(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Boostrapper).Assembly));

            return services;
        }
    }
}
