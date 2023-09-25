using Domain.Services.Bus;
using Domain.Services.Bus.Messages;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services.Bus.Consumers
{
    public class ConsumerBus : IConsumerBus
    {
        private readonly ILogger<ConsumerBus> _logger;
        private readonly IConnection _connection;
        private IModel _channel;

        private readonly string _queueName = "paymentprocessed-queue";

        public ConsumerBus(ILogger<ConsumerBus> logger, IConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        public void Consume()
        {
            ConnectToRabbitMQ();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, eventArgs) =>
            {
                var content = string.Empty;

                try
                {
                    content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                    var message = JsonSerializer.Deserialize<PaymentProcessedMessage>(content);

                    await OnMessage(this, new ConsumerBusEvent(message));

                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error when trying to process the message. Content: {content}. ErrorMessage: {ex?.Message}");

                    _channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: true);
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        public event AsyncEventHandler<ConsumerBusEvent> OnMessage;

        private void ConnectToRabbitMQ()
        {
            if (_channel is object && _channel.IsOpen)
                return;

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false);
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection.Close();
            _connection.Dispose();
        }
    }
}
