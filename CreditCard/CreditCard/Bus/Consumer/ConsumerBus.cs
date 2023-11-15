using System.Text.Json;
using MassTransit;
using Messages;

namespace CreditCard.Bus.Consumer
{
    public class ConsumerBus : IConsumer<CreditRequestedMessage>
    {
        private readonly ILogger<ConsumerBus> _logger;

        public ConsumerBus(ILogger<ConsumerBus> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreditRequestedMessage> context)
        {
            CreditRequestedMessage message = null!;

            try
            {
                message = context.Message;

                CreditAnalysisService.Analyze(message, out PaymentProcessedMessage paymentProcessedMessage);

                await context.Publish(paymentProcessedMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to process the message. Content: {JsonSerializer.Serialize(message)}. ErrorMessage: {ex?.Message}");
            }
        }
    }
}
