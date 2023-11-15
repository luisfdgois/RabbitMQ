namespace Messages;

public record CreditRequestedMessage(Guid OrderId,
                                     string Number,
                                     string CVV,
                                     int NumberOfInstallment,
                                     decimal ValuePerInstallment) : BusMessage { }