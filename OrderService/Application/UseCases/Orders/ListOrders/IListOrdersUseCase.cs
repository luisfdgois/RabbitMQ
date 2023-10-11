using Application.UseCases.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.UseCases.Orders.ListOrders
{
    public interface IListOrdersUseCase
    {
        Task<List<OrderDto>> Execute();
    }
}
