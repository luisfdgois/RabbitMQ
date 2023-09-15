using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Application.Mapping.Extensions
{
    public static class PaymentTypeFactory
    {
        public static Payment CreatePaymentObject(this JObject jObject, PaymentType paymentType)
        {
            var deserializationFunctions = GetPaymentTypesDeserializationFunction();

            if (!deserializationFunctions.ContainsKey(paymentType)) throw new InvalidPaymentTypeConversionException();

            var function = deserializationFunctions[paymentType];

            return function.Invoke(jObject.ToString());
        }

        private static IDictionary<PaymentType, Func<string, Payment>> GetPaymentTypesDeserializationFunction()
        {
            return new Dictionary<PaymentType, Func<string, Payment>> { { PaymentType.CreditCard, _deserialize<CreditCard> } };

            T _deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json);
        }
    }
}
