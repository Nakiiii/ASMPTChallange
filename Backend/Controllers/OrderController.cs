using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            _logger.LogInformation($"Getting all Orders...");
            var orders = await _orderService.GetAllAsync();
            return Ok(_mapper.Map<List<OrderDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            _logger.LogInformation($"Getting Orders with id: {id}...");
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(OrderDto dto)
        {
            _logger.LogInformation($"Creating Order...");
            var order = _mapper.Map<Order>(dto);
            await _orderService.AddAsync(order, dto.BoardIds);
            _logger.LogInformation($"Order {order.Id} created successfully...");

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, _mapper.Map<OrderDto>(order));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, OrderDto dto)
        {
            _logger.LogInformation($"Updating Order {id}...");
            if (id != dto.Id)
            {
                _logger.LogWarning("Update failed: ID mismatch {Id}...", id);
                return BadRequest("ID mismatch");
            }
            var order = _mapper.Map<Order>(dto);
            await _orderService.UpdateAsync(order, dto.BoardIds);
            _logger.LogInformation("Order {Id} updated successfully...", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting Order {Id}...", id);
            await _orderService.DeleteAsync(id);
            _logger.LogInformation("Order {Id} deleted successfully...", id);
            return NoContent();
        }

        [HttpPost("{orderId}/boards/{boardId}")]
        public async Task<IActionResult> AddBoard(Guid orderId, Guid boardId)
        {
            _logger.LogInformation("Adding Board to Order...");
            await _orderService.AddBoardToOrderAsync(orderId, boardId);
            return NoContent();
        }

        [HttpDelete("{orderId}/boards/{boardId}")]
        public async Task<IActionResult> RemoveBoard(Guid orderId, Guid boardId)
        {
            _logger.LogInformation("Removing Board from Order...");
            await _orderService.RemoveBoardFromOrderAsync(orderId, boardId);
            return NoContent();
        }

    }
}
