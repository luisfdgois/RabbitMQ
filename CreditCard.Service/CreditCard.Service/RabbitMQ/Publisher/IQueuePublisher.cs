namespace CreditCard.Service.RabbitMQ.Publisher
{
    public interface IQueuePublisher
    {
        bool Publish(string message);
    }
}
