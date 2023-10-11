using Domain.Process.UpdateOrderPayment;
using Domain.Services.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Background
{
    public class PaymentProcessedWorker : BackgroundService
    {
        private readonly ILogger<PaymentProcessedWorker> _logger;
        private readonly IServiceProvider _provider;
        private readonly IConsumerBus _consumer;

        public PaymentProcessedWorker(ILogger<PaymentProcessedWorker> logger, IServiceProvider provider, IConsumerBus consumer)
        {
            _logger = logger;
            _provider = provider;
            _consumer = consumer;

            _consumer.OnMessage += UpdatedPaymentStatus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Consume();

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await Task.CompletedTask;
        }

        private async Task UpdatedPaymentStatus(object sender, ConsumerBusEvent args)
        {
            using IServiceScope scope = _provider.CreateAsyncScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            try
            {
                var command = new UpdateOrderPaymentCommand(args.Message.OrderId, args.Message.PaymentApproved);

                await mediator.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when trying to update order payment status.");
            }
        }
    }
}
