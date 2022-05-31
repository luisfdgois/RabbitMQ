using Domain.Models.Enums;

namespace Domain.Models.DTOs
{
    public abstract class BusMessageDto
    {
        public virtual PaymentTypeDto PaymentType { get; }
    }
}
