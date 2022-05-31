using Application.UseCases.Models.Requests;
using Application.UseCases.Models.Responses;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Models.DTOs;

namespace Application.Shared.Mapping
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
                .ForMember(dest => dest.Payment, opt => opt.Ignore());
        }

        private void DomainToDomainDto()
        {
            CreateMap<CreditCard, CreditCardMessageDto>();

            CreateMap<Payment, BusMessageDto>()
                .Include(typeof(CreditCard), typeof(CreditCardMessageDto));
        }

        private void DomainToResponseDto()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(s => s.CreationDate));
        }
    }
}
