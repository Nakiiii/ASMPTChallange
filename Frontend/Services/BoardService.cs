using Frontend.Models;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class BoardService
    {
        private readonly HttpClient _http;

        public BoardService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Board>?> GetBoardsAsync()
        {
            return await _http.GetFromJsonAsync<List<Board>>("api/board");
        }

        public async Task<Board?> GetBoardAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<Board>($"api/board/{id}");
        }

        public async Task<bool> CreateBoardAsync(Board board)
        {
            var response = await _http.PostAsJsonAsync("api/board", board);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateBoardAsync(Guid id, Board board)
        {
            var response = await _http.PutAsJsonAsync($"api/board/{id}", board);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBoardAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/board/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
