using Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Settings;

namespace Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Contracts
{
    public interface IPublisherQueue
    {
        bool IsMatch(AvailableQueue availableQueue);
        bool Publish(string jsonContent);
    }
}
