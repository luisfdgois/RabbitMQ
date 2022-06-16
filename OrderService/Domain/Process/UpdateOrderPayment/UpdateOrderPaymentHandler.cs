using Domain.Repositories;
using MediatR;

namespace Domain.Process.UpdateOrderPayment
{
    public class UpdateOrderPaymentHandler : IRequestHandler<UpdateOrderPaymentCommand>
    {
        private readonly IOrderRepository _repository;

        public UpdateOrderPaymentHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateOrderPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetById(request.OrderId);

            if (order is null) throw new Exception("Order not found.");

            order.UpdatePaymentStatus(request.Approved);

            await _repository.SaveChangesAsync();

            return await Task.FromResult(Unit.Value);
        }
    }
}
