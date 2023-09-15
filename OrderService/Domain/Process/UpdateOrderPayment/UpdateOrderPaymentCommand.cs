using MediatR;
using System;

namespace Domain.Process.UpdateOrderPayment
{
    public class UpdateOrderPaymentCommand : IRequest
    {
        public Guid OrderId { get; private set; }
        public bool Approved { get; private set; }

        public UpdateOrderPaymentCommand(Guid orderId, bool approved)
        {
            OrderId = orderId;
            Approved = approved;
        }
    }
}
