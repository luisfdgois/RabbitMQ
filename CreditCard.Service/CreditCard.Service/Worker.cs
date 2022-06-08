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

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task OnMessageAsync(object sender, QueueConsumerEventArgs args)
        {
            _logger.LogInformation($"New Message. OrderId: {args.Message.OrderId}");

            var result = CreditAnalysisService.Analyze(args.Message);

            var message = result.ToString();

            _publisher.Publish(message);
        }
    }
}