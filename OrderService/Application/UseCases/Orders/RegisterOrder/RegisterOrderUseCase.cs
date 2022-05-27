using Application.Shared.Extensions;
using Application.UseCases.Models.Requests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Orders.RegisterOrder
{
    public class RegisterOrderUseCase : IRegisterOrderUseCase
    {
        private readonly IMapper _mapper;
        private readonly DbContext _context;

        public RegisterOrderUseCase(IMapper mapper, OrderContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Execute(RegisterOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto)
                               .AddPaymentMethod(dto.Payment.Deserializer(dto.PaymentType));

            await _context.Set<Order>().AddAsync(order);

            await _context.SaveChangesAsync();
        }
    }
}
