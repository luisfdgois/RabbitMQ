using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Orders.GetOrderById
{
    public class GetOrderByIdRequestHandler : IRequestHandler<GetOrderByIdRequest, GetOrderByIdResponse>
    {
        private readonly DbContext _dbContext;

        public GetOrderByIdRequestHandler(OrderContext context)
        {
            _dbContext = context;
        }

        public async Task<GetOrderByIdResponse> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Order>().AsNoTrackingWithIdentityResolution()
                       .Include(o => o.Payment)
                       .Where(o => o.Id == request.id)
                       .Select(o => new GetOrderByIdResponse(o.Id, o.Payment.Status.ToString(), o.ProductDescription, o.ProductValue, o.ProductQuantity, o.UserEmail, o.CreatedOn))
                       .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
