using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ChallangeDbContext _context;
        private readonly ILogger<BoardRepository> _logger;

        public BoardRepository(ChallangeDbContext context, ILogger<BoardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Board> AddAsync(Board board, List<Guid>? orderIds = null, List<Guid>? componentIds = null)
        {
            _logger.LogInformation("Creating Board...");
            await AddOrdersAndComponentsToBoard(board, orderIds, componentIds);
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Board Created!");
            return board;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting Board...");
            var board = await _context.Boards.FindAsync(id);
            if (board == null) return false;
            _context.Boards.Remove(board);
            _logger.LogInformation("Board Deleted!");
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Board>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all Boards...");
            return await _context.Boards
            .Include(b => b.Components)
            .ToListAsync();
        }

        public async Task<Board?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching Board: {BoardId}", id);
            return await _context.Boards
            .Include(b => b.Components)
            .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> UpdateAsync(Board board, List<Guid>? orderIds = null, List<Guid>? componentIds = null)
        {
            _logger.LogInformation("Updating board: {BoardId}", board.Id);
            await AddOrdersAndComponentsToBoard(board, orderIds, componentIds);
            _context.Boards.Update(board);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddComponentAsync(Guid boardId, Guid componentId)
        {
            _logger.LogInformation("Adding Component to Board...");
            try
            {
                var board = await GetByIdAsync(boardId);
                if (board == null) throw new Exception("Board not found");

                var component = await _context.Components.FindAsync(componentId);
                if (component == null) throw new Exception("Component not found");

                if (!board.Components.Contains(component))
                    board.Components.Add(component);

                _logger.LogInformation("Adding Component to Board succeeded!");
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Adding Component to Board failed!\n{ex}", ex);
                return false;
            }
        }

        public async Task<bool> RemoveComponentAsync(Guid boardId, Guid componentId)
        {
            _logger.LogInformation("Removing Component from Board...");
            try
            {
                var board = await GetByIdAsync(boardId);
                if (board == null) throw new Exception("Board not found");

                var component = board.Components.FirstOrDefault(c => c.Id == componentId);
                if (component == null) throw new Exception("Component not associated with board");

                board.Components.Remove(component);
                _logger.LogInformation("Removing Component from Board succeeded!");
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Removing Component from Board failed!\n{ex}", ex);
                return false;
            }
        }

        private async Task AddOrdersAndComponentsToBoard(Board board, List<Guid>? orderIds = null, List<Guid>? componentIds = null)
        {
            if (orderIds != null)
            {
                var orders = await _context.Orders
                    .Where(o => orderIds.Contains(o.Id))
                    .ToListAsync();

                board.Orders = orders;
            }

            if (componentIds != null)
            {
                var components = await _context.Components
                    .Where(c => componentIds.Contains(c.Id))
                    .ToListAsync();

                board.Components = components;
            }
        }
    }
}
