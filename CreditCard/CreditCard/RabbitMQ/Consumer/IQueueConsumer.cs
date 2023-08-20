using RabbitMQ.Client.Events;

namespace CreditCard.RabbitMQ.Consumer
{
    public interface IQueueConsumer : IDisposable
    {
        void Consume();

        event AsyncEventHandler<QueueConsumerEventArgs> OnMessage;
    }
}
