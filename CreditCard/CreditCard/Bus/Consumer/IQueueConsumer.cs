using RabbitMQ.Client.Events;

namespace CreditCard.Bus.Consumer
{
    public interface IQueueConsumer : IDisposable
    {
        void Consume();

        event AsyncEventHandler<QueueConsumerEventArgs> OnMessage;
    }
}
