using Application.Shared.Helpers.Factories;
using Application.UseCases.Orders.ListOrders;
using Application.UseCases.Orders.RegisterOrder;
using Domain.Entities;
using Domain.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IRegisterOrderUseCase, RegisterOrderUseCase>();
            services.AddScoped<IListOrdersUseCase, ListOrdersUseCase>();

            services.AddScoped<IPaymentTypeFactory>(_ =>
            {
                var paymentTypeFactory = new PaymentTypeFactory();
                paymentTypeFactory.RegisterPaymentType<CreditCard>(PaymentType.CreditCard);

                return paymentTypeFactory;
            });

            return services;
        }
    }
}
