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

        public async Task<Board> AddBoardAsync(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<bool> DeleteBoardAsync(Guid id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null) return false;
            _context.Boards.Remove(board);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Board>> GetAllBoardsAsync()
        {
            return await _context.Boards
            .Include(b => b.Components)
            .ToListAsync();
        }

        public async Task<Board?> GetBoardByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching Board {BoardId}", id);
            return await _context.Boards
            .Include(b => b.Components)
            .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> UpdateBoardAsync(Guid id, Board board)
        {
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Board {BoardId} updated successfully", id);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Concurrency issue updating board {boardId}", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating board {OrderId}", id);
                return false;
            }
        }
    }
}
