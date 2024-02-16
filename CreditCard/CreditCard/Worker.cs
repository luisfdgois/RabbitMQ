using CreditCard.Models;
using CreditCard.RabbitMQ.Consumer;
using CreditCard.RabbitMQ.Publisher;

namespace CreditCard
{
    public class Worker : BackgroundService
    {
        private readonly IQueueConsumer _consumer;
        private readonly IQueuePublisher _publisher;
        private readonly ILogger<Worker> _logger;

        public Worker(IQueueConsumer consumer, IQueuePublisher publisher, ILogger<Worker> logger)
        {
            _consumer = consumer;
            _publisher = publisher;
            _logger=logger;

            _consumer.OnMessage += OnMessageAsync;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            _consumer.Consume();

            _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");
        }

        private async Task OnMessageAsync(object sender, QueueConsumerEventArgs args)
        {
            await Task.Yield();

            _logger.LogInformation($"New Message. OrderId: {args.Message.OrderId}");

            CreditAnalysisService.Analyze(args.Message, out ProcessedCreditMessage processedCredit);

            _publisher.Publish(message: processedCredit.ToString());
        }
    }
}