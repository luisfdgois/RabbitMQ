using MediatR;

namespace Domain.Process.UpdateOrderPayment
{
    public record UpdateOrderPaymentCommand (Guid OrderId, bool Approved) : IRequest;
}
