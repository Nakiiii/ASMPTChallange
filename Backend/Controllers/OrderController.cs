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
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            _logger.LogInformation($"Creating Order...");
            var createdOrder = await _orderService.AddOrderAsync(order);
            _logger.LogInformation($"Order {order.Id} created successfully...");
            return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderDto orderDto)
        {
            _logger.LogInformation($"Updating Order {id}...");
            if (id != orderDto.Id)
            {
                _logger.LogWarning("Update failed: ID mismatch {Id}...", id);
                return BadRequest("ID mismatch");
            }

            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                _logger.LogWarning("Update failed: Order {Id} not found...", id);
                return NotFound();
            }

            _mapper.Map(orderDto, existingOrder);

            var updated = await _orderService.UpdateOrderAsync(id, existingOrder);
            if (!updated)
            {
                _logger.LogError("Failed to update order {Id}...", id);
                return StatusCode(500, "Could not update the order");
            }

            _logger.LogInformation("Order {Id} updated successfully...", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting Order {Id}...", id);
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result) return NotFound();
            _logger.LogInformation("Order {Id} deleted successfully...", id);
            return NoContent();
        }

    }
}
