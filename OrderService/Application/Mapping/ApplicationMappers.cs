using AutoMapper;
using Domain.Entities;
using Messages;

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
            CreateMap<CreditCard, CreditRequestedMessage>();

            CreateMap<Payment, BusMessage>()
                .Include(typeof(CreditCard), typeof(CreditRequestedMessage));
        }
    }
}
