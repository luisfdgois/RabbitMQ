using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Domain
{
    public static class Boostrapper
    {
        public static IServiceCollection AddDomainProcess(this IServiceCollection services)
        {
            services.AddMediatR(config => config.AsScoped(), AppDomain.CurrentDomain.Load("Domain"));

            return services;
        }
    }
}
