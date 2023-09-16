namespace Domain.Services.Bus.Messages
{
    public class PaymentProcessedMessage
    {
        public Guid OrderId { get; set; }
        public bool PaymentApproved { get; set; }
    }
}
