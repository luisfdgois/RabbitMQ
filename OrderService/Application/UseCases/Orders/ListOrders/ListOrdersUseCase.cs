using Application.UseCases.Models.Responses;
using AutoMapper;
using Domain.Repositories;

namespace Application.UseCases.Orders.ListOrders
{
    public class ListOrdersUseCase : IListOrdersUseCase
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;

        public ListOrdersUseCase(IMapper mapper, IOrderRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<OrderDto>> Execute()
        {
            return _mapper.Map<List<OrderDto>>(await _repository.GetAll());
        }
    }
}
