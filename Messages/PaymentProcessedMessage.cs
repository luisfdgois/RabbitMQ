namespace Messages;

public record PaymentProcessedMessage(Guid OrderId, bool PaymentApproved) : BusMessage { }