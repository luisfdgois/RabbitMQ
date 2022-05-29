using Infrastructure.External.RabbitMQ.Settings;

namespace Infrastructure.External.RabbitMQ.Contracts
{
    public interface IQueue
    {
        public bool IsMatch(AvailableQueue availableQueue);
        bool Publish(string jsonContent);
    }
}
