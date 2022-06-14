using Domain.Models.Enums;

namespace Domain.Models.DTOs
{
    public abstract class BusMessage
    {
        public virtual PaymentTypeDto PaymentType { get; }
    }
}
