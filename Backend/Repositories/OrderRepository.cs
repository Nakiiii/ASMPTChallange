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

        public async Task<Order> AddAsync(Order order, List<Guid>? boardIds = null)
        {
            _logger.LogInformation("Creating Order...");
            await PopulateNavigationPropertiesAsync(order, boardIds);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Order Created!");
            return order;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting Order...");
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            _logger.LogInformation("Order Removed!");
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all Orders");
            return await _context.Orders.Include(o => o.Boards).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching order: {OrderId}", id);
            return await _context.Orders.Include(o => o.Boards).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> UpdateAsync(Order order, List<Guid>? boardIds = null)
        {
            _logger.LogInformation("Updating order: {OrderId}", order.Id);
            await PopulateNavigationPropertiesAsync(order, boardIds);
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddBoardAsync(Guid orderId, Guid boardId)
        {
            _logger.LogInformation("Adding Board to Order...");
            try
            {
                var order = await GetByIdAsync(orderId);
                if (order == null) throw new Exception("Order not found");

                var board = await _context.Boards.FindAsync(boardId);
                if (board == null) throw new Exception("Board not found");

                if (!order.Boards.Contains(board))
                    order.Boards.Add(board);

                _logger.LogWarning("Adding Board to Order succeeded!");
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex) 
            {
                _logger.LogWarning("Adding Board to Order failed!\n{ex}", ex);
                return false;
            }
        }

        public async Task<bool> RemoveBoardAsync(Guid orderId, Guid boardId)
        {
            _logger.LogInformation("Removing Board from Order...");
            try
            {
                var order = await GetByIdAsync(orderId);
                if (order == null) throw new Exception("Order not found");

                var board = order.Boards.FirstOrDefault(b => b.Id == boardId);
                if (board == null) throw new Exception("Board not associated with order");

                order.Boards.Remove(board);

                _logger.LogInformation("Removing Board from Order succeeded!");
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex) 
            {
                _logger.LogWarning("Removing Board from Order failed!\n{ex}", ex);
                return false;
            }
        }

        private async Task PopulateNavigationPropertiesAsync(Order order, List<Guid>? boardIds)
        {
            if (boardIds != null)
            {
                var boards = await _context.Boards
                    .Where(b => boardIds.Contains(b.Id))
                    .ToListAsync();

                order.Boards = boards;
            }
        }
    }
}
