using CreditCard.Models;

namespace CreditCard.Bus.Consumer
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
