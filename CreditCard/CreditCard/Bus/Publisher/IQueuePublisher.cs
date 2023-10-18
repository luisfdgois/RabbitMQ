namespace CreditCard.Bus.Publisher
{
    public interface IQueuePublisher
    {
        bool Publish(string message);
    }
}
