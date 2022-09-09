using Application.Shared.Helpers.Factories;
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
        private readonly IPaymentTypeFactory _paymentFactory;
        private readonly IPublisherServiceBus _serviceBus;
        private readonly IOrderRepository _repository;

        public RegisterOrderUseCase(IMapper mapper, IPaymentTypeFactory paymentFactory,
            IPublisherServiceBus serviceBus, IOrderRepository repository)
        {
            _mapper = mapper;
            _paymentFactory = paymentFactory;
            _serviceBus = serviceBus;
            _repository = repository;
        }

        public async Task Execute(RegisterOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);

            var payment = _paymentFactory.DeserializeJson(dto.PaymentType, dto.Payment.ToString());
            order.AddPaymentMethod(payment);

            await _repository.Add(order);

            var busMessage = _mapper.Map<BusMessage>(order.Payment);

            _serviceBus.Publish(busMessage);

            await _repository.SaveChangesAsync();
        }
    }
}
