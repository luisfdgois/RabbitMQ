using Domain.Enums;
using Newtonsoft.Json.Linq;

namespace Application.UseCases.Orders.RegisterOrder
{
    public record RegisterOrderRequest(string ProductDescription,
                                       decimal ProductValue,
                                       int ProductQuantity,
                                       string UserEmail,
                                       PaymentType PaymentType,
                                       JObject Payment);
}
