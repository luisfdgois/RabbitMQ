using Application.UseCases.Models.Requests;
using Application.UseCases.Models.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Shared.Mapping
{
    public class ApplicationMappers : Profile
    {
        public ApplicationMappers()
        {
            RequestDtoToDomain();
            DomainToResponseDto();
        }

        private void RequestDtoToDomain()
        {
            CreateMap<RegisterPaymentDto, Payment>();
            CreateMap<RegisterCreditCardDto, CreditCard>();

            CreateMap<RegisterOrderDto, Order>()
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(s => s.Payment));
        }

        private void DomainToResponseDto()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(s => s.CreationDate));
        }
    }
}
