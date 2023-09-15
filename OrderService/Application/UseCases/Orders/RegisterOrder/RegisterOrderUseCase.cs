using Application.UseCases.Models.Requests;
using AutoMapper;
using Domain.Entities;
using Domain.Models.DTOs;
using Domain.Repositories;
using Domain.Services.Bus;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
                var order = _mapper.Map<Order>(dto);

                await _repository.Add(order);

                var message = _mapper.Map<BusMessage>(order.Payment);

                _publisherBus.Publish(message);

                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The order registration action could not be completed.");
            }
        }
    }
}
