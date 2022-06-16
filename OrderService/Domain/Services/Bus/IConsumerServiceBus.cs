using RabbitMQ.Client.Events;

namespace Domain.Services.Bus
{
    public interface IConsumerServiceBus : IDisposable
    {
        void Consume();
        
        event AsyncEventHandler<ConsumerBusEvent> OnMessage;
    }
}
