using Domain.Entities;
using Domain.Models.Enums;

namespace Application.Shared.Helpers.Factories
{
    public interface IPaymentTypeFactory
    {
        Payment DeserializeJson(PaymentType paymentType, string json);
    }
}
