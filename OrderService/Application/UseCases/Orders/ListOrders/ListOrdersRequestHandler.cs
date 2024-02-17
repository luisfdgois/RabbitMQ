using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Orders.ListOrders
{
    public class ListOrdersRequestHandler : IRequestHandler<ListOrdersRequest, List<ListOrdersResponse>>
    {
        private readonly DbContext _dbContext;

        public ListOrdersRequestHandler(OrderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ListOrdersResponse>> Handle(ListOrdersRequest request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Order>()
                       .Include(o => o.Payment)
                       .AsNoTrackingWithIdentityResolution()
                       .Select(o => new ListOrdersResponse(o.Id, o.Payment.Status.ToString(), o.ProductDescription))
                       .ToListAsync();
        }
    }
}
