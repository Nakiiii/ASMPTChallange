using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ChallangeDbContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ChallangeDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
            .Include(o => o.Boards)
            .ThenInclude(b => b.Components)
            .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching order {OrderId}", id);
            return await _context.Orders
            .Include(o => o.Boards)
            .ThenInclude(b => b.Components)
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> UpdateOrderAsync(Guid id, Order order)
        {
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order {OrderId} updated successfully", order.Id);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Concurrency issue updating order {OrderId}", order.Id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating order {OrderId}", order.Id);
                return false;
            }
        }
    }
}
