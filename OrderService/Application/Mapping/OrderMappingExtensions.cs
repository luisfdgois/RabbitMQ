using Application.Factories;
using Application.UseCases.Orders.RegisterOrder;
using Domain.Entities;

namespace Application.Mapping
{
    public static class OrderMappingExtensions
    {
        public static Order MapToDomainEntity(this RegisterOrderRequest dto)
        {
            var payment = dto.Payment.ConvertToPaymentObject(dto.PaymentType);

            return new Order(productDescription: dto.ProductDescription,
                             productValue: dto.ProductValue,
                             productQuantity: dto.ProductQuantity,
                             userEmail: dto.UserEmail,
                             payment: payment);
        }
    }
}
