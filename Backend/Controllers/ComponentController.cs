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
        public async Task<ActionResult<List<ComponentDto>>> GetAll()
        {
            _logger.LogInformation($"Getting all Components...");
            var components = await _componentService.GetAllAsync();
            return Ok(_mapper.Map<List<ComponentDto>>(components));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentDto>> GetById(Guid id)
        {
            _logger.LogInformation($"Getting Component with id: {id}...");
            var component = await _componentService.GetByIdAsync(id);
            if (component == null) return NotFound();
            return Ok(_mapper.Map<ComponentDto>(component));
        }

        [HttpPost]
        public async Task<ActionResult<ComponentDto>> Create(ComponentDto dto)
        {
            _logger.LogInformation($"Creating Component...");
            var component = _mapper.Map<Component>(dto);
            await _componentService.AddAsync(component);
            return CreatedAtAction(nameof(GetById), new { id = component.Id }, _mapper.Map<ComponentDto>(component));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ComponentDto dto)
        {
            _logger.LogInformation($"Updating Component with id: {id}...");
            if (id != dto.Id) return BadRequest("ID mismatch");
            var component = _mapper.Map<Component>(dto);
            await _componentService.UpdateAsync(component);
            _logger.LogInformation($"Component {id} updated successfully...");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"Deleting Component with {id}...");

            await _componentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
