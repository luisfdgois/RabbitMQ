using System.Text.Json;

namespace CreditCard.Models
{
    public record ProcessedCreditMessage
    {
        public Guid OrderId { get; init; }
        public bool PaymentApproved { get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
