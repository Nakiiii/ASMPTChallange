using Backend.Data;
using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<List<Order>> GetAllAsync() => _orderRepository.GetAllAsync();
        public Task<Order?> GetByIdAsync(Guid id) => _orderRepository.GetByIdAsync(id);
        public Task<Order> AddAsync(Order order, List<Guid>? boardIds = null) => _orderRepository.AddAsync(order, boardIds);
        public Task<bool> UpdateAsync(Order order, List<Guid>? boardIds = null) => _orderRepository.UpdateAsync(order, boardIds);
        public Task<bool> DeleteAsync(Guid id) => _orderRepository.DeleteAsync(id);
        public Task<bool> AddBoardToOrderAsync(Guid orderId, Guid boardId) => _orderRepository.AddBoardAsync(orderId, boardId);
        public Task<bool> RemoveBoardFromOrderAsync(Guid orderId, Guid boardId) => _orderRepository.RemoveBoardAsync(orderId, boardId);
    }
}
