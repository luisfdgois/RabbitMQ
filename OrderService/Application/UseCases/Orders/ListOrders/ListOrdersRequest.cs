using MediatR;

namespace Application.UseCases.Orders.ListOrders
{
    public record ListOrdersRequest : IRequest<List<ListOrdersResponse>>;
}
