using Domain.Entities;
using Domain.Process.UpdateOrderPayment;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Orders.UpdateOrderPayment
{
    public class UpdateOrderPaymentHandler : IRequestHandler<UpdateOrderPaymentCommand>
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<UpdateOrderPaymentHandler> _logger;

        public UpdateOrderPaymentHandler(OrderContext dbContext, ILogger<UpdateOrderPaymentHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Set<Order>().Include(o => o.Payment).FirstOrDefaultAsync(o => o.Id.Equals(request.OrderId), cancellationToken);

            if (order is null)
                _logger.LogError($"It could not possible to find an Order with Key = {request.OrderId}");
            else
            {
                order.UpdatePaymentStatus(request.Approved);

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Order = {request.OrderId}. Paymente Result - Approved = {request.Approved}");
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
