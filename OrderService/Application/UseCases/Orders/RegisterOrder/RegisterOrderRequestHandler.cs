using Application.Mapping;
using AutoMapper;
using Domain.Services.Bus;
using Infrastructure.Data;
using MediatR;
using Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Orders.RegisterOrder
{
    public class RegisterOrderRequestHandler : IRequestHandler<RegisterOrderRequest>
    {
        private readonly IMapper _mapper;
        private readonly IPublisherBus _publisherBus;
        private readonly DbContext _dbContext;
        private readonly ILogger<RegisterOrderRequestHandler> _logger;

        public RegisterOrderRequestHandler(IMapper mapper, IPublisherBus publisherBus, OrderContext dbContex, ILogger<RegisterOrderRequestHandler> logger)
        {
            _mapper = mapper;
            _publisherBus = publisherBus;
            _dbContext = dbContex;
            _logger = logger;
        }

        public async Task Handle(RegisterOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = request.MapToDomainEntity();

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
