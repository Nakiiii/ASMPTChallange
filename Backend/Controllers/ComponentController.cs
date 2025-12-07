using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;
        private readonly IMapper _mapper;
        private readonly ILogger<ComponentController> _logger;

        public ComponentController(IComponentService componentService, IMapper mapper, ILogger<ComponentController> logger)
        {
            _componentService = componentService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Getting all Components...");
            var components = await _componentService.GetAllComponentsAsync();
            return Ok(components);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation($"Getting Component with id: {id}...");
            var component = await _componentService.GetComponentByIdAsync(id);
            if (component == null) return NotFound();

            return Ok(component);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Component component)
        {
            _logger.LogInformation($"Creating Component...");
            var created = await _componentService.AddComponentAsync(component);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ComponentDto componentDto)
        {
            _logger.LogInformation($"Updating Component with id: {id}...");
            if (id != componentDto.Id)
            {
                _logger.LogWarning($"Failed updating Component with id: {id}...");
                return BadRequest("ID mismatch");
            }

            var existingComponent = await _componentService.GetComponentByIdAsync(id);
            if (existingComponent == null) return NotFound();

            _mapper.Map(componentDto, existingComponent);

            var updated = await _componentService.UpdateComponentAsync(id, existingComponent);
            if (!updated)
            {
                _logger.LogWarning($"Failed updating Component with id: {id}...");
                return StatusCode(500, "Could not update the component");
            }

            _logger.LogInformation($"Component {id} updated successfully...");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"Deleting Component with {id}...");

            var deleted = await _componentService.DeleteComponentAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
