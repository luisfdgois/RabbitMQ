using Domain.Process.UpdateOrderPayment;
using MassTransit;
using MediatR;
using Messages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Services.Bus.Consumers
{
    public class ConsumerBus : IConsumer<PaymentProcessedMessage>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ConsumerBus> _logger;

        public ConsumerBus(ILogger<ConsumerBus> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<PaymentProcessedMessage> context)
        {
            PaymentProcessedMessage message = null!;

            try
            {
                message = context.Message;

                var command = new UpdateOrderPaymentCommand(message.OrderId, message.PaymentApproved);

                await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to process the message. Content: {JsonSerializer.Serialize(message)}. ErrorMessage: {ex?.Message}");
            }
        }
    }
}
