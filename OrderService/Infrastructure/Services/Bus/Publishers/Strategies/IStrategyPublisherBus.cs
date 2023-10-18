using Domain.Services.Bus.Messages;

namespace Infrastructure.Services.Bus.Publishers.Strategies
{
    public interface IStrategyPublisherBus
    {
        /// <summary>
        /// This method checks whether the message parameter is compatible with the implemented strategy. 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool IsMatch(BusMessage message);
        Task<bool> Publish(BusMessage message);
    }
}
