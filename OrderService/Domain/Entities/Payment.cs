using Domain.Enums;

namespace Domain.Entities
{
    public class Payment : Entity
    {
        public Guid OrderId { get; private set; }
        public Order? Order { get; private set; }
        public PaymentStatus Status { get; private set; }

        protected Payment() : base()
        {
            Status = PaymentStatus.Pending;
        }

        public void UpdateStatus(bool approved)
        {
            if (approved)
                Status = PaymentStatus.Approved;
            else
                Status = PaymentStatus.Denied;

            LastUpdate = DateTime.UtcNow;
        }
    }
}
