namespace CreditCard.RabbitMQ.Publisher
{
    public interface IQueuePublisher
    {
        bool Publish(string message);
    }
}
