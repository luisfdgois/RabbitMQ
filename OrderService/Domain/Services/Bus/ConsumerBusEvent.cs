using Domain.Models.DTOs;

namespace Domain.Services.Bus
{
    public class ConsumerBusEvent : EventArgs
    {
        public PaymentProcessedMessage Message { get; }

        public ConsumerBusEvent(PaymentProcessedMessage message)
        {
            Message = message;
        }
    }
}
