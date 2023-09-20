using CreditCard.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CreditCard.RabbitMQ.Consumer
{
    public class QueueConsumer : IQueueConsumer
    {
        private readonly IConnection _connection;
        private IModel _channel;
        private readonly ILogger<QueueConsumer> _logger;

        private readonly string _queueName = "creditcard-queue";
        private readonly string _exchangeName = "order-exchange";
        private readonly string _routingKey = "creditcard";

        public QueueConsumer(IConnection connection, ILogger<QueueConsumer> logger)
        {
            _connection = connection;
            _logger = logger;

            Connect();
        }

        public void Consume()
        {
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, eventArgs) =>
            {
                var content = string.Empty;

                try
                {
                    content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                    var message = JsonSerializer.Deserialize<CreditCardMessage>(content);

                    await OnMessage(this, new QueueConsumerEventArgs(message!));

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

        private void Connect()
        {
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false);

            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);

            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _routingKey);
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}
