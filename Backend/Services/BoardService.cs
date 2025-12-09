using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public Task<List<Board>> GetAllAsync() => _boardRepository.GetAllAsync();
        public Task<Board?> GetByIdAsync(Guid id) => _boardRepository.GetByIdAsync(id);
        public Task<Board> AddAsync(Board board, List<Guid>? orderIds = null, List<Guid>? componentIds = null) => _boardRepository.AddAsync(board, orderIds, componentIds);
        public Task<bool> UpdateAsync(Board board, List<Guid>? orderIds = null, List<Guid>? componentIds = null) => _boardRepository.UpdateAsync(board, orderIds, componentIds);
        public Task<bool> DeleteAsync(Guid id) => _boardRepository.DeleteAsync(id);
        public Task<bool> AddComponentToBoardAsync(Guid boardId, Guid componentId) => _boardRepository.AddComponentAsync(boardId, componentId);
        public Task<bool> RemoveComponentFromBoardAsync(Guid boardId, Guid componentId) => _boardRepository.RemoveComponentAsync(boardId, componentId);
    }
}
