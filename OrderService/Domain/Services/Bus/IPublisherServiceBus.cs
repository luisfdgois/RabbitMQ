using Domain.Models.DTOs;

namespace Domain.Services.Bus
{
    public interface IPublisherServiceBus
    {
        bool Publish(BusMessageDto busMessage);
    }
}
