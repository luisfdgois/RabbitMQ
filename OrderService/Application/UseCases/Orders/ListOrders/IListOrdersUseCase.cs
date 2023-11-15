using Application.UseCases.Orders.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Orders.ListOrders
{
    public interface IListOrdersUseCase
    {
        Task<List<OrderDto>> Execute();
    }
}
