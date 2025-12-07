using Frontend.Models;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class OrderService
    {
        private readonly HttpClient _http;

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Order>> GetOrdersAsync() =>
            await _http.GetFromJsonAsync<List<Order>>("api/order") ?? new();

        public async Task<Order?> GetOrderAsync(Guid id) =>
            await _http.GetFromJsonAsync<Order>($"api/order/{id}");

        public async Task CreateOrderAsync(Order order) =>
            await _http.PostAsJsonAsync("api/order", order);

        public async Task UpdateOrderAsync(Guid id, Order order) =>
            await _http.PutAsJsonAsync($"api/order/{order.Id}", order);

        public async Task DeleteOrderAsync(Guid id) =>
            await _http.DeleteAsync($"api/order/{id}");
    }
}
