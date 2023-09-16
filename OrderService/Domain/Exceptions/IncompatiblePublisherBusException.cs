namespace Domain.Exceptions
{
    public class IncompatiblePublisherBusException : Exception
    {
        public IncompatiblePublisherBusException(string messageType) : base($"This publisher bus is only compatible with the {messageType}") { }
    }
}
