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

        public Task<List<Order>> GetAllOrdersAsync() => _orderRepository.GetAllOrdersAsync();

        public Task<Order?> GetOrderByIdAsync(Guid id) => _orderRepository.GetOrderByIdAsync(id);

        public Task<Order> AddOrderAsync(Order order) => _orderRepository.AddOrderAsync(order);

        public Task<bool> UpdateOrderAsync(Guid id, Order order) => _orderRepository.UpdateOrderAsync(id, order);

        public Task<bool> DeleteOrderAsync(Guid id) => (_orderRepository.DeleteOrderAsync(id));
    }
}
