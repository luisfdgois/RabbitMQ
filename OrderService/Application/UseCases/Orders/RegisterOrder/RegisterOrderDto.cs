using Domain.Enums;
using Newtonsoft.Json.Linq;

namespace Application.UseCases.Orders.RegisterOrder
{
    public record RegisterOrderDto(string ProductDescription, decimal ProductValue, int ProductQuantity, string UserEmail, PaymentType PaymentType, JObject Payment);
}
