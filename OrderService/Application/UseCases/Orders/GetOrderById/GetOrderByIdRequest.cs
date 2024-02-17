using MediatR;

namespace Application.UseCases.Orders.GetOrderById
{
    public record GetOrderByIdRequest(Guid id) : IRequest<GetOrderByIdResponse>;
}
