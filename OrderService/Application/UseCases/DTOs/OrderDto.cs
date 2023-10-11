using Domain.Enums;

namespace Application.UseCases.DTOs
{
    public record OrderDto(Guid Id, PaymentStatus PaymentStatus, string ProductDescription,
        decimal ProductValue, int ProductQuantity, string UserEmail, DateTime CreatedOn, DateTime LastUpdate);
}
