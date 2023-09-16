using Application.Factories;
using Application.UseCases.Models.Requests;
using Domain.Entities;

namespace Application.Mapping
{
    public static class OrderMappingExtensions
    {
        public static Order MapToOrder(this RegisterOrderDto dto)
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
