using Application.Mapping;
using AutoMapper;
using Domain.Services.Bus;
using Infrastructure.Data;
using Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Orders.RegisterOrder
{
    public class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        private readonly IMapper _mapper;
        private readonly IPublisherBus _publisherBus;
        private readonly DbContext _dbContext;
        private readonly ILogger<RegisterOrderUseCase> _logger;

        public RegisterOrderUseCase(IMapper mapper, IPublisherBus publisherBus, OrderContext dbContex, ILogger<RegisterOrderUseCase> logger)
        {
            _mapper = mapper;
            _publisherBus = publisherBus;
            _dbContext = dbContex;
            _logger = logger;
        }

        public async Task Execute(RegisterOrderDto dto)
        {
            try
            {
                var order = dto.MapToDomainEntity();

                await _dbContext.AddAsync(order);

                await _dbContext.SaveChangesAsync();

                var message = _mapper.Map<BusMessage>(order.Payment);

                await _publisherBus.Publish(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The order registration action could not be completed.");
            }
        }
    }
}
