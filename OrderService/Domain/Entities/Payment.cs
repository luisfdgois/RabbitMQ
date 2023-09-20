using Domain.Exceptions;
using System;

namespace Domain.Entities
{
    public class Payment : Entity
    {
        public bool Approved { get; private set; }

        public Guid OrderId { get; private set; }
        public Order? Order { get; private set; }

        protected Payment() : base() { }

        public void AssignOrder(Order order)
        {
            ArgumentNullException.ThrowIfNull(nameof(order));

            if (Order is not null) throw new OrderAlreadyAssignedException();

            OrderId = order.Id;
            Order = order;
        }

        public void UpdateStatus(bool approved)
        {
            Approved = approved;
        }
    }
}
