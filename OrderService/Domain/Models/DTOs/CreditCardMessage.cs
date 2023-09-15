using Domain.Models.DTOs;
using System;
using System.Text.Json;

namespace Domain.DTOs
{
    public class CreditCardMessage : BusMessage
    {
        public Guid OrderId { get; set; }
        public string Number { get; private set; }
        public string CVV { get; private set; }
        public int NumberOfInstallment { get; private set; }
        public decimal ValuePerInstallment { get; private set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
