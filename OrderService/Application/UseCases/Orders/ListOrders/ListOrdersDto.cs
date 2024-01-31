using Domain.Enums;

namespace Application.UseCases.Orders.ListOrders
{
    public record ListOrdersDto(Guid Id, PaymentStatus PaymentStatus, string ProductDescription);
}
