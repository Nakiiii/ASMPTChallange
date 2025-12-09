using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IBoardService
    {
        Task<List<Board>> GetAllAsync();
        Task<Board?> GetByIdAsync(Guid id);
        Task<Board> AddAsync(Board board, List<Guid>? orderIds = null, List<Guid>? componentIds = null);
        Task<bool> UpdateAsync(Board board, List<Guid>? orderIds = null, List<Guid>? componentIds = null);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddComponentToBoardAsync(Guid boardId, Guid componentId);
        Task<bool> RemoveComponentFromBoardAsync(Guid boardId, Guid componentId);
    }
}
