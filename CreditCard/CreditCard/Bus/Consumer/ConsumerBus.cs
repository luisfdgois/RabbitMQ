using System.Text.Json;
using MassTransit;
using Messages;

namespace CreditCard.Bus.Consumer
{
    public class ConsumerBus : IConsumer<CreditRequestedMessage>
    {
        private readonly ILogger<ConsumerBus> _logger;
        private const string DestinationQueue = "paymentprocessed-queue";

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

                var endpoint = await context.GetSendEndpoint(new Uri($"queue:{DestinationQueue}"));

                await endpoint.Send(paymentProcessedMessage, context => context.Durable = true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to process the message. Content: {JsonSerializer.Serialize(message)}. ErrorMessage: {ex?.Message}");
            }
        }
    }
}
