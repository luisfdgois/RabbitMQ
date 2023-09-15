using Application.Mapping.Extensions;
using Application.UseCases.Models.Requests;
using Application.UseCases.Models.Responses;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Models.DTOs;

namespace Application.Mapping
{
    public class ApplicationMappers : Profile
    {
        public ApplicationMappers()
        {
            RequestDtoToDomain();
            DomainToDomainDto();
            DomainToResponseDto();
        }

        private void RequestDtoToDomain()
        {
            CreateMap<RegisterOrderDto, Order>()
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment.CreatePaymentObject(src.PaymentType)));
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
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(s => s.CreatedOn))
                .ForMember(dest => dest.Approved, opt => opt.MapFrom(s => s.Payment.Approved));
        }
    }
}
