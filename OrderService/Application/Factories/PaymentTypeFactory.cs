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
            var type = Type.GetType($"Domain.Entities.{paymentType}, Domain");

            if (type is null)
                throw new InvalidPaymentTypeConversionException();

            var paymentObject = (Payment)JsonSerializer.Deserialize(jObject.ToString(), type)!;

            return paymentObject;
        }
    }
}
