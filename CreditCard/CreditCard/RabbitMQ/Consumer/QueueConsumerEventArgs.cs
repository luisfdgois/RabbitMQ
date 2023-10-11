using CreditCard.Models;

namespace CreditCard.RabbitMQ.Consumer
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
