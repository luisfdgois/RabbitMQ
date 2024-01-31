using Domain.Enums;

namespace Application.UseCases.Orders.GetOrderById
{
    public record GetOrderByIdDto(Guid Id, PaymentStatus PaymentStatus, string ProductDescription,
        decimal ProductValue, int ProductQuantity, string UserEmail, DateTime CreatedOn);
}
