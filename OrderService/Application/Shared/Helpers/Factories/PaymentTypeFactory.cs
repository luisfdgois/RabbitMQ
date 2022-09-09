using Domain.Entities;
using Domain.Models.Enums;
using System.Text.Json;

namespace Application.Shared.Helpers.Factories
{
    public class PaymentTypeFactory : IPaymentTypeFactory
    {
        private IDictionary<PaymentType, Func<string, Payment>> _payments;

        public PaymentTypeFactory()
        {
            _payments = new Dictionary<PaymentType, Func<string, Payment>>();
        }

        public Payment DeserializeJson(PaymentType paymentType, string json) =>
            _payments[paymentType].Invoke(json);

        public void RegisterPaymentType<T>(PaymentType paymentType) where T : Payment
        {
            if(_payments.ContainsKey(paymentType))
                throw new Exception("This payment type is already being used.");

            _payments[paymentType] = _deserialize;

            T _deserialize(string json) => JsonSerializer.Deserialize<T>(json);
        }
    }
}
