using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContext _context;
        private bool _disposed;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task Add(Order order)
        {
            await _context.Set<Order>().AddAsync(order);
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Set<Order>().Include(o => o.Payment)
                                              .ToListAsync();
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Set<Order>().Include(o => o.Payment)
                                              .FirstOrDefaultAsync(o => o.Id.Equals(id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();

            _disposed = true;
        }
    }
}
