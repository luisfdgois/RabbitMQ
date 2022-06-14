using Domain.Models.DTOs;
using Domain.Services.Bus;
using Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Contracts;
using Infrastructure.ComplementaryServices.RabbitMQ.Publishers.Settings;

namespace Infrastructure.Services
{
    public class PublisherServiceBus : IPublisherServiceBus
    {
        private readonly IEnumerable<IPublisherQueue> _queues;

        public PublisherServiceBus(IEnumerable<IPublisherQueue> queues)
        {
            _queues = queues;
        }

        public bool Publish(BusMessage busMessage)
        {
            var jsonContent = busMessage.ToString();

            var queue = _queues.FirstOrDefault(q => q.IsMatch((AvailableQueue)busMessage.PaymentType));

            if (queue is object) return queue.Publish(jsonContent);

            return false;
        }
    }
}
