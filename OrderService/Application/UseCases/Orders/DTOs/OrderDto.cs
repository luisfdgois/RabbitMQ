using Domain.Enums;

namespace Application.UseCases.Orders.DTOs
{
    public record OrderDto(Guid Id, PaymentStatus PaymentStatus, string ProductDescription,
        decimal ProductValue, int ProductQuantity, string UserEmail, DateTime CreatedOn, DateTime LastUpdate);
}
