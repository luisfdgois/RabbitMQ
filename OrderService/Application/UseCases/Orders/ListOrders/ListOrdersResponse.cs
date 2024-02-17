using Domain.Enums;

namespace Application.UseCases.Orders.ListOrders
{
    public record ListOrdersResponse(Guid Id, PaymentStatus PaymentStatus, string ProductDescription);
}
