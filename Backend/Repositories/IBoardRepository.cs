using Backend.Models;

namespace Backend.Repositories
{
    public interface IBoardRepository
    {
        Task<List<Board>> GetAllAsync();
        Task<Board?> GetByIdAsync(Guid id);
        Task<Board> AddAsync(Board board, List<Guid>? orderIds = null, List<Guid>? componentIds = null);
        Task<bool> UpdateAsync(Board board, List<Guid>? orderIds = null, List<Guid>? componentIds = null);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddComponentAsync(Guid boardId, Guid componentId);
        Task<bool> RemoveComponentAsync(Guid boardId, Guid componentId);
    }
}
