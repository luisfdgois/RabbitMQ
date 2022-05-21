using Application.UseCases.Models.Responses;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Orders.ListOrders
{
    public class ListOrdersUseCase : IListOrdersUseCase
    {
        private readonly IMapper _mapper;
        private readonly DbContext _context;

        public ListOrdersUseCase(IMapper mapper, OrderContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<OrderDto>> Execute()
        {
            return _mapper.Map<List<OrderDto>>(await _context.Set<Order>().ToListAsync());
        }
    }
}
