using Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Settings;

namespace Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Contracts
{
    public interface IPublisherQueue
    {
        QueueMessage QueueMessage { get; }
        bool Publish(string jsonContent);
    }
}
