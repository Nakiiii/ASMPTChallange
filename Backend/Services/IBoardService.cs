using Backend.Models;

namespace Backend.Services
{
    public interface IBoardService
    {
        Task<List<Board>> GetAllBoardsAsync();
        Task<Board?> GetBoardByIdAsync(Guid id);
        Task<Board> AddBoardAsync(Board board);
        Task<bool> UpdateBoardAsync(Guid id, Board board);
        Task<bool> DeleteBoardAsync(Guid id);
    }
}
