using Application.UseCases.Models.Responses;
using AutoMapper;
using Domain.Entities;
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
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(s => s.CreatedOn))
                .ForMember(dest => dest.Approved, opt => opt.MapFrom(s => s.Payment.Approved));
        }
    }
}
