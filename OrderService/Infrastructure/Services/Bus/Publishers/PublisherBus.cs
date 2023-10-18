using Domain.Exceptions;
using Domain.Services.Bus;
using Domain.Services.Bus.Messages;
using Infrastructure.Services.Bus.Publishers.Strategies;

namespace Infrastructure.Services.Bus.Publishers
{
    public class PublisherBus : IPublisherBus
    {
        private readonly IEnumerable<IStrategyPublisherBus> _publishers;

        public PublisherBus(IEnumerable<IStrategyPublisherBus> publishers)
        {
            _publishers = publishers;
        }

        public async Task Publish(BusMessage busMessage)
        {
            var bus = _publishers.SingleOrDefault(b => b.IsMatch(busMessage));

            if (bus is not object) throw new InvalidPublisherBusException();

            await bus.Publish(busMessage);
        }
    }
}
