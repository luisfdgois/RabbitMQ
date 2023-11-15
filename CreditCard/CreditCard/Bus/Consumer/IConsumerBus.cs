using RabbitMQ.Client.Events;

namespace CreditCard.Bus.Consumer
{
    public interface IConsumerBus : IDisposable
    {
        void Consume();
        event AsyncEventHandler<QueueConsumerEventArgs> OnMessage;
    }
}
