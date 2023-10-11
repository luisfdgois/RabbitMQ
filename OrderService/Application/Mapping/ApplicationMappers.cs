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
        }

        private void DomainToDomainDto()
        {
            CreateMap<CreditCard, CreditCardMessage>();

            CreateMap<Payment, BusMessage>()
                .Include(typeof(CreditCard), typeof(CreditCardMessage));
        }
    }
}
