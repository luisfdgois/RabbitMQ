using Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Settings;

namespace Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Contracts
{
    public interface IPublisherQueue
    {
        AvailableQueue Queue { get; }
        bool Publish(string jsonContent);
    }
}
