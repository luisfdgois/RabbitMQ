namespace Domain.Services.Bus.Messages
{
    public record CreditCardMessage(Guid OrderId, string Number, string CVV, int NumberOfInstallment, decimal ValuePerInstallment) : BusMessage { }
}
