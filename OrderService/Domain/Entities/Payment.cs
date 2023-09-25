using Domain.Enums;
using Domain.Exceptions;

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

        public void AssignOrder(Order order)
        {
            ArgumentNullException.ThrowIfNull(nameof(order));

            if (Order is not null) throw new OrderAlreadyAssignedException();

            OrderId = order.Id;
            Order = order;
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
