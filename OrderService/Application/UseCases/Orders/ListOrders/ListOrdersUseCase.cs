using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Orders.ListOrders
{
    public class ListOrdersUseCase : IListOrdersUseCase
    {
        private readonly DbContext _dbContext;

        public ListOrdersUseCase(OrderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ListOrdersDto>> Execute(ListOrdersRequest request, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Order>()
                                   .Include(o => o.Payment)
                                   .AsNoTrackingWithIdentityResolution()
                                   .Select(o => new ListOrdersDto(o.Id, o.Payment.Status, o.ProductDescription))
                                   .Skip(request.Skip())
                                   .Take(request.Take())
                                   .ToListAsync(cancellationToken);
        }
    }
}
