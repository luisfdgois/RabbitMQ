using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Application.Factories
{
    public static class PaymentTypeFactory
    {
        public static Payment ConvertToPaymentObject(this JObject jObject, PaymentType paymentType)
        {
            var func = GetDeserializationFunctionByPaymentType(paymentType);

            return func.Invoke(jObject.ToString());
        }

        private static Func<string, Payment> GetDeserializationFunctionByPaymentType(PaymentType paymentType)
        {
            return paymentType switch
            {
                PaymentType.CreditCard => _deserialize<CreditCard>,
                _ => throw new InvalidPaymentTypeConversionException()
            };

            T _deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json);
        }
    }
}
