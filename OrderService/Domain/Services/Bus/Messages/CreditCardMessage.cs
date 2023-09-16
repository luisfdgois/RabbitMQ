using System;
using System.Text.Json;

namespace Domain.Services.Bus.Messages
{
    public record CreditCardMessage : BusMessage
    {
        public Guid OrderId { get; set; }
        public string Number { get; private set; }
        public string CVV { get; private set; }
        public int NumberOfInstallment { get; private set; }
        public decimal ValuePerInstallment { get; private set; }

        public CreditCardMessage(Guid orderId, string number, string cVV, int numberOfInstallment, decimal valuePerInstallment)
        {
            OrderId = orderId;
            Number = number;
            CVV = cVV;
            NumberOfInstallment = numberOfInstallment;
            ValuePerInstallment = valuePerInstallment;
        }

        public override string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
