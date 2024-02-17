using Domain.Enums;

namespace Application.UseCases.Orders.GetOrderById
{
    public record GetOrderByIdResponse(Guid Id, string PaymentStatus, string ProductDescription,
        decimal ProductValue, int ProductQuantity, string UserEmail, DateTime CreatedOn);
}
