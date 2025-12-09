using Frontend.Models;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class ComponentService
    {
        private readonly HttpClient _http;

        public ComponentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Component>?> GetComponentsAsync()
        {
            return await _http.GetFromJsonAsync<List<Component>>("api/component") ?? new List<Component>();
        }

        public async Task<Component?> GetComponentAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<Component>($"api/component/{id}");
        }

        public async Task<bool> CreateComponentAsync(Component component)
        {
            var response = await _http.PostAsJsonAsync("api/component", component);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateComponentAsync(Guid id, Component component)
        {
            var response = await _http.PutAsJsonAsync($"api/component/{id}", component);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteComponentAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/component/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
