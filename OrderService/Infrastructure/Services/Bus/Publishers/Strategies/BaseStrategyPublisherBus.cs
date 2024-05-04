using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Bus.Publishers.Strategies
{
    public abstract class BaseStrategyPublisherBus
    {
        protected readonly IBus _bus;
        protected readonly ILogger<BaseStrategyPublisherBus> _logger;

        protected BaseStrategyPublisherBus(IBus bus, ILogger<BaseStrategyPublisherBus> logger)
        {
            _bus = bus;
            _logger = logger;
        }
    }
}
