using Domain.Services.Bus.Messages;

namespace Domain.Services.Bus
{
    public interface IPublisherBus
    {
        Task Publish(BusMessage busMessage);
    }
}
