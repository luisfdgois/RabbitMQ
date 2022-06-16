using Application.Shared.Extensions;
using Application.UseCases.Models.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Models.DTOs;
using Domain.Repositories;
using Domain.Services.Bus;

namespace Application.UseCases.Orders.RegisterOrder
{
    public class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        private readonly IMapper _mapper;
        private readonly IPublisherServiceBus _serviceBus;
        private readonly IOrderRepository _repository;

        public RegisterOrderUseCase(IMapper mapper, IPublisherServiceBus serviceBus, IOrderRepository repository)
        {
            _mapper = mapper;
            _serviceBus = serviceBus;
            _repository = repository;
        }

        public async Task Execute(RegisterOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto)
                               .AddPaymentMethod(dto.Payment.Deserializer(dto.PaymentType));

            await _repository.Add(order);

            var busMessage = _mapper.Map<BusMessage>(order.Payment);

            _serviceBus.Publish(busMessage);

            await _repository.SaveChangesAsync();
        }
    }
}
