using Messages;

namespace Domain.Services.Bus
{
    public interface IPublisherBus
    {
        Task Publish(BusMessage busMessage);
    }
}
