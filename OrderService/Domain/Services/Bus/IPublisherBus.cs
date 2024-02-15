using Domain.Services.Bus.Messages;

namespace Domain.Services.Bus
{
    public interface IPublisherBus
    {
        void Publish(BusMessage busMessage);
    }
}
