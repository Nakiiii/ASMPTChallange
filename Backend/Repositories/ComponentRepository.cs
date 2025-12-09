using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly ChallangeDbContext _context;
        private readonly ILogger<ComponentRepository> _logger;

        public ComponentRepository(ChallangeDbContext context, ILogger<ComponentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Component> AddAsync(Component component)
        {
            _logger.LogInformation("Creating Component...");
            _context.Components.Add(component);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Component Created!");
            return component;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting Component...");
            var component = await _context.Components.FindAsync(id);
            if (component == null) return false;
            _context.Components.Remove(component);
            _logger.LogInformation("Component Deleted!");
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Component>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all Components...");
            return await _context.Components.ToListAsync();
        }

        public async Task<Component?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching Component {id}", id);
            return await _context.Components.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateAsync(Component component)
        {
            _logger.LogInformation("Updating Component: {ComponentId}", component.Id);
            _context.Components.Update(component);
            _logger.LogInformation("Component Updated!");
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
