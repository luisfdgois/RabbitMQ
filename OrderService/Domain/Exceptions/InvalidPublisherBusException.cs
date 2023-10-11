namespace Domain.Exceptions
{
    public class InvalidPublisherBusException : Exception
    {
        public InvalidPublisherBusException() : base("It could not find the publisher bus related to message type") { }
    }
}
