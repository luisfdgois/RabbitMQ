using Application.UseCases.Models.Responses;

namespace Application.UseCases.Orders.ListOrders
{
    public interface IListOrdersUseCase
    {
        Task<List<OrderDto>> Execute();
    }
}
