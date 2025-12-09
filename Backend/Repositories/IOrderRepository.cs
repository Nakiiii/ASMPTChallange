using Backend.Models;

namespace Backend.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order> AddAsync(Order order, List<Guid>? boardIds = null);
        Task<bool> UpdateAsync(Order order, List<Guid>? boardIds = null);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddBoardAsync(Guid orderId, Guid boardId);
        Task<bool> RemoveBoardAsync(Guid orderId, Guid boardId);
    }
}
