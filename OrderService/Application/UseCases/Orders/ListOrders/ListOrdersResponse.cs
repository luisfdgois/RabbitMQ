namespace Application.UseCases.Orders.ListOrders
{
    public record ListOrdersResponse(Guid Id, string PaymentStatus, string ProductDescription);
}
