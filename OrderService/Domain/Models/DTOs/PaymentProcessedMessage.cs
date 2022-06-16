namespace Domain.Models.DTOs
{
    public class PaymentProcessedMessage
    {
        public Guid OrderId { get; set; }
        public bool PaymentApproved { get; set; }
    }
}
