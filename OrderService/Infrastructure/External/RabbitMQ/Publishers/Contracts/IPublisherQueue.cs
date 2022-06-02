using Infrastructure.External.RabbitMQ.Publishers.Settings;

namespace Infrastructure.External.RabbitMQ.Publishers.Contracts
{
    public interface IPublisherQueue
    {
        bool IsMatch(AvailableQueue availableQueue);
        bool Publish(string jsonContent);
    }
}
