using Application.UseCases.Models.Enums;
using Domain.Entities;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Application.Shared.Extensions
{
    public static class PaymentExtensions
    {
        public static Payment Deserializer(this JObject paymentJson, PaymentTypeDto paymentType)
        {
            Payment? payment = (paymentType) switch { 
                                             PaymentTypeDto.CreditCard => JsonSerializer.Deserialize<CreditCard>(paymentJson.ToString()),
                                             _ => null
                                             };

            return payment;
        }
    }
}
