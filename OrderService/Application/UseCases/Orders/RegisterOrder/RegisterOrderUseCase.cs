using Application.Mapping;
using Application.UseCases.Models.Requests;
using AutoMapper;
using Domain.Repositories;
using Domain.Services.Bus;
using Domain.Services.Bus.Messages;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Orders.RegisterOrder
{
    public class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        private readonly IMapper _mapper;
        private readonly IPublisherBus _publisherBus;
        private readonly IOrderRepository _repository;
        private readonly ILogger<RegisterOrderUseCase> _logger;

        public RegisterOrderUseCase(IMapper mapper, IPublisherBus publisherBus, IOrderRepository repository, ILogger<RegisterOrderUseCase> logger)
        {
            _mapper = mapper;
            _publisherBus = publisherBus;
            _repository = repository;
            _logger = logger;
        }

        public async Task Execute(RegisterOrderDto dto)
        {
            try
            {
                var order = dto.MapToOrder();

                await _repository.Add(order);

                var message = _mapper.Map<BusMessage>(order.Payment);

                await _repository.SaveChangesAsync();

                await _publisherBus.Publish(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The order registration action could not be completed.");
            }
        }
    }
}
