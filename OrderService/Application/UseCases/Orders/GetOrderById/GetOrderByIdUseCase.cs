using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Orders.GetOrderById
{
    public class GetOrderByIdUseCase : IGetOrderByIdUseCase
    {
        private readonly DbContext _dbContext;

        public GetOrderByIdUseCase(OrderContext context)
        {
            _dbContext = context;
        }

        public async Task<GetOrderByIdDto> Execute(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Order>().AsNoTrackingWithIdentityResolution()
                                   .Include(o => o.Payment)
                                   .Where(o => o.Id == id)
                                   .Select(o => new GetOrderByIdDto(o.Id, o.Payment.Status, o.ProductDescription, o.ProductValue, o.ProductQuantity, o.UserEmail, o.CreatedOn))
                                   .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
