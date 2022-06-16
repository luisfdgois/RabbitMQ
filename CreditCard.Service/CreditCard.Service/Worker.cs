using CreditCard.Service.Models;
using CreditCard.Service.RabbitMQ.Consumer;
using CreditCard.Service.RabbitMQ.Publisher;

namespace CreditCard.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueueConsumer _consumer;
        private readonly IQueuePublisher _publisher;

        public Worker(ILogger<Worker> logger, IQueueConsumer consumer, IQueuePublisher publisher)
        {
            _logger = logger;
            _consumer = consumer;
            _publisher = publisher;

            _consumer.OnMessage += OnMessageAsync;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Consume();

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await Task.CompletedTask;
        }

        private async Task OnMessageAsync(object sender, QueueConsumerEventArgs args) =>

            await Task.Run(() =>
            {
                _logger.LogInformation($"New Message. OrderId: {args.Message.OrderId}");

                CreditAnalysisService.Analyze(args.Message, out ProcessedCreditMessage processedCredit);

                _publisher.Publish(message: processedCredit.ToString());
            });
    }
}