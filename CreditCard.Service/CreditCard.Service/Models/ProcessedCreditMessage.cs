using System.Text.Json;

namespace CreditCard.Service.Models
{
    public class ProcessedCreditMessage
    {
        public Guid OrderId { get; set; }
        public bool PaymentApproved { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
