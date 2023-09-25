using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Process.UpdateOrderPayment
{
    public class UpdateOrderPaymentHandler : IRequestHandler<UpdateOrderPaymentCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<UpdateOrderPaymentHandler> _logger;

        public UpdateOrderPaymentHandler(IOrderRepository repository, ILogger<UpdateOrderPaymentHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetById(request.OrderId);

            if (order is null)
                _logger.LogError($"It could not possible to find an Order with Key = {request.OrderId}");
            else
            {
                order.UpdatePaymentStatus(request.Approved);

                await _repository.SaveChangesAsync();

                _logger.LogInformation($"Order = {request.OrderId}. Paymente Result - Approved = {request.Approved}");
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
