using Backend.Models;

namespace Backend.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order> AddAsync(Order order, List<Guid>? boardIds = null);
        Task<bool> UpdateAsync(Order order, List<Guid>? boardIds = null);
        Task<bool> DeleteAsync(Guid id);
        public Task<bool> AddBoardToOrderAsync(Guid orderId, Guid boardId);
        public Task<bool> RemoveBoardFromOrderAsync(Guid orderId, Guid boardId);
    }
}
