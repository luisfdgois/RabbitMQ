using CreditCard.Service.RabbitMQ.Consumer;

namespace CreditCard.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueueConsumer _consumer;

        public Worker(ILogger<Worker> logger, IQueueConsumer consumer)
        {
            _logger = logger;
            _consumer = consumer;

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
            _logger.LogInformation("New Message. \n" +
                $"OrderId: {args.Message.OrderId}");
        }
    }
}