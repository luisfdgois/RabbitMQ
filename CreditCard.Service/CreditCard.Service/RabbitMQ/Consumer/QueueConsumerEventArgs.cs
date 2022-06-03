using CreditCard.Service.RabbitMQ.Consumer.Models;

namespace CreditCard.Service.RabbitMQ.Consumer
{
    public class QueueConsumerEventArgs : EventArgs 
    {
        public CreditCardMessage Message { get; }

        public QueueConsumerEventArgs(CreditCardMessage message)
        {
            Message = message;
        }
    }
}
