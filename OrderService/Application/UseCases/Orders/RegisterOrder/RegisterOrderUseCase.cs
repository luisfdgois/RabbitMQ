using Application.Shared.Extensions;
using Application.UseCases.Models.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Models.DTOs;
using Domain.Services.Bus;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Orders.RegisterOrder
{
    public class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        private readonly IMapper _mapper;
        private readonly IPublisherServiceBus _serviceBus;
        private readonly DbContext _context;

        public RegisterOrderUseCase(IMapper mapper, IPublisherServiceBus serviceBus, OrderContext context)
        {
            _mapper = mapper;
            _serviceBus = serviceBus;
            _context = context;
        }

        public async Task Execute(RegisterOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto)
                               .AddPaymentMethod(dto.Payment.Deserializer(dto.PaymentType));

            await _context.Set<Order>().AddAsync(order);

            var busMessage = _mapper.Map<BusMessage>(order.Payment);

            _serviceBus.Publish(busMessage);

            await _context.SaveChangesAsync();
        }
    }
}
