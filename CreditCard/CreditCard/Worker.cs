using CreditCard.Models;
using CreditCard.RabbitMQ.Consumer;
using CreditCard.RabbitMQ.Publisher;

namespace CreditCard
{
    public class Worker : BackgroundService
    {
        private readonly IQueueConsumer _consumer;
        private readonly IQueuePublisher _publisher;

        public Worker(IQueueConsumer consumer, IQueuePublisher publisher)
        {
            _consumer = consumer;
            _publisher = publisher;

            _consumer.OnMessage += OnMessageAsync;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Consume();

            Console.Write($"Worker running at: {DateTimeOffset.Now}");

            await Task.CompletedTask;
        }

        private async Task OnMessageAsync(object sender, QueueConsumerEventArgs args) =>

            await Task.Run(() =>
            {
                Console.Write($"New Message. OrderId: {args.Message.OrderId}");

                CreditAnalysisService.Analyze(args.Message, out ProcessedCreditMessage processedCredit);

                _publisher.Publish(message: processedCredit.ToString());
            });
    }
}