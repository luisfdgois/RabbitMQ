using RabbitMQ.Client.Events;

namespace CreditCard.Service.RabbitMQ.Consumer
{
    public interface IQueueConsumer : IDisposable
    {
        void Consume();

        event AsyncEventHandler<QueueConsumerEventArgs> OnMessage;
    }
}
