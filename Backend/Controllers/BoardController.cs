using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly IMapper _mapper;
        private readonly ILogger<BoardController> _logger;

        public BoardController(IBoardService boardService, IMapper mapper, ILogger<BoardController> logger)
        {
            _boardService = boardService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<BoardDto>>> GetAll()
        {
            _logger.LogInformation("Getting all Boards...");
            var boards = await _boardService.GetAllAsync();
            return Ok(_mapper.Map<List<BoardDto>>(boards));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BoardDto>> GetById(Guid id)
        {
            _logger.LogInformation($"Getting Board with id: {id}...");
            var board = await _boardService.GetByIdAsync(id);
            if (board == null) return NotFound();
            return Ok(_mapper.Map<BoardDto>(board));
        }

        [HttpPost]
        public async Task<ActionResult<BoardDto>> Create(BoardDto dto)
        {
            _logger.LogInformation("Creating board...");
            var board = _mapper.Map<Board>(dto);
            await _boardService.AddAsync(board, dto.OrderIds, dto.ComponentIds);
            _logger.LogInformation("Board {BoardId} created successfully", board.Id);
            return CreatedAtAction(nameof(GetById), new { id = board.Id }, _mapper.Map<BoardDto>(board));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BoardDto dto)
        {
            _logger.LogInformation($"Updating Board with id: {id}...");
            if (id != dto.Id)
            {
                _logger.LogError($"Failed updating Board with id: {id}...");
                return BadRequest("ID mismatch.");
            }
            var board = _mapper.Map<Board>(dto);
            await _boardService.UpdateAsync(board, dto.OrderIds, dto.ComponentIds);
            _logger.LogInformation($"Updating Board with id: {id} successful!");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"Deleting Board with id: {id}...");

            await _boardService.DeleteAsync(id);
            _logger.LogInformation("Board {Id} deleted successfully...", id);

            return NoContent();
        }

        [HttpPost("{boardId}/components/{componentId}")]
        public async Task<IActionResult> AddComponent(Guid boardId, Guid componentId)
        {
            await _boardService.AddComponentToBoardAsync(boardId, componentId);
            return NoContent();
        }

        [HttpDelete("{boardId}/components/{componentId}")]
        public async Task<IActionResult> RemoveComponent(Guid boardId, Guid componentId)
        {
            await _boardService.RemoveComponentFromBoardAsync(boardId, componentId);
            return NoContent();
        }
    }
}
