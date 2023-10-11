using RabbitMQ.Client.Events;
using System;

namespace Domain.Services.Bus
{
    public interface IConsumerBus : IDisposable
    {
        void Consume();
        
        event AsyncEventHandler<ConsumerBusEvent> OnMessage;
    }
}
