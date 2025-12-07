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
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all Boards...");
            var boards = await _boardService.GetAllBoardsAsync();
            return Ok(boards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation($"Getting Board with id: {id}...");
            var board = await _boardService.GetBoardByIdAsync(id);
            if (board == null) return NotFound();

            return Ok(board);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Board board)
        {
            _logger.LogInformation("Creating board...");
            var created = await _boardService.AddBoardAsync(board);
            _logger.LogInformation("Board {BoardId} created successfully", board.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BoardDto boardDto)
        {
            _logger.LogInformation($"Updating Board with id: {id}...");
            if (id != boardDto.Id)
            {
                _logger.LogError($"Failed updating Board with id: {id}...");
                return BadRequest("ID mismatch.");
            }

            var existingBoard = await _boardService.GetBoardByIdAsync(id);
            if (existingBoard == null)
            {
                _logger.LogError("Failed to update board {Id}", id);
                return NotFound();
            }

            _mapper.Map(boardDto, existingBoard);

            var updated = await _boardService.UpdateBoardAsync(id, existingBoard);
            if (!updated)
            {
                _logger.LogError("Failed to update board {Id}", id);
                return StatusCode(500, "Could not update the board");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"Deleting Board with id: {id}...");

            var deleted = await _boardService.DeleteBoardAsync(id);
            if (!deleted) return NotFound();

            _logger.LogInformation("Board {Id} deleted successfully...", id);

            return NoContent();
        }
    }
}
