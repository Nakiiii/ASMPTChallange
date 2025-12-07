using Backend.Models;
using Backend.Repositories;

namespace Backend.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public Task<List<Board>> GetAllBoardsAsync() => _boardRepository.GetAllBoardsAsync();
        public Task<Board?> GetBoardByIdAsync(Guid id) => _boardRepository.GetBoardByIdAsync(id);
        public Task<Board> AddBoardAsync(Board board) => _boardRepository.AddBoardAsync(board);
        public Task<bool> UpdateBoardAsync(Guid id, Board board) => _boardRepository.UpdateBoardAsync(id, board);
        public Task<bool> DeleteBoardAsync(Guid id) => _boardRepository.DeleteBoardAsync(id);
    }
}
