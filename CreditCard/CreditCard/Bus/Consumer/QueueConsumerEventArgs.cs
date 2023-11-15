using Messages;

namespace CreditCard.Bus.Consumer
{
    public class QueueConsumerEventArgs : EventArgs
    {
        public CreditRequestedMessage Message { get; }

        public QueueConsumerEventArgs(CreditRequestedMessage message)
        {
            Message = message;
        }
    }
}
