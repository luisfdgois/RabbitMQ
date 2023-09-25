using Application.UseCases.Models.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Services.Bus.Messages;

namespace Application.Mapping
{
    public class ApplicationMappers : Profile
    {
        public ApplicationMappers()
        {
            DomainToDomainDto();
            DomainToResponseDto();
        }

        private void DomainToDomainDto()
        {
            CreateMap<CreditCard, CreditCardMessage>();

            CreateMap<Payment, BusMessage>()
                .Include(typeof(CreditCard), typeof(CreditCardMessage));
        }

        private void DomainToResponseDto()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Approved, opt => opt.MapFrom(s => s.Payment.Status == PaymentStatus.Approved ? true : false));
        }
    }
}
