using Domain.Models.DTOs;
using Domain.Services.Bus;
using Infrastructure.External.RabbitMQ.Contracts;
using Infrastructure.External.RabbitMQ.Settings;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class PublisherServiceBus : IPublisherServiceBus
    {
        private readonly IEnumerable<IQueue> _queues;

        public PublisherServiceBus(IEnumerable<IQueue> queues)
        {
            _queues = queues;
        }

        public bool Publish(BusMessageDto busMessage)
        {
            var jsonContent = busMessage.ToString();

            if (_queues.FirstOrDefault(q => q.IsMatch((AvailableQueue)busMessage.PaymentType)) is IQueue queue
                && queue is object)
                return queue.Publish(jsonContent);

            return false;
        }
    }
}
