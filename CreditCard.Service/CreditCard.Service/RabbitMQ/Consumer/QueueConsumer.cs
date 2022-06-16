using CreditCard.Service.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CreditCard.Service.RabbitMQ.Consumer
{
    public class QueueConsumer : IQueueConsumer
    {
        private readonly ILogger<QueueConsumer> _logger;
        private readonly IConnection _connection;
        private IModel _channel;

        private readonly string _queueName = "creditcard-queue";

        public QueueConsumer(ILogger<QueueConsumer> logger, IConnection connection)
        {
            _logger = logger;
            _connection = connection;

            ConnectToRabbitMQ();
        }

        public void Consume()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, eventArgs) =>
            {
                var content = string.Empty;

                try
                {
                    content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                    var message = JsonSerializer.Deserialize<CreditCardMessage>(content);

                    await OnMessage(this, new QueueConsumerEventArgs(message));

                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error trying to process the message. Content: {content}. ErrorMessage: {ex?.Message}");

                    _channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: true);
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        public event AsyncEventHandler<QueueConsumerEventArgs> OnMessage;

        private void ConnectToRabbitMQ()
        {
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "creditcard-queue", durable: true, exclusive: false, autoDelete: false);
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}
