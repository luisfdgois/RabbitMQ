namespace Domain.Services.Bus.Messages
{
    public abstract record BusMessage
    {
        public abstract string ToJson();
    }
}
