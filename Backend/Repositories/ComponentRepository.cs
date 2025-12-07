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

        public async Task<Component> AddComponentAsync(Component component)
        {
            _context.Components.Add(component);
            await _context.SaveChangesAsync();
            return component;
        }

        public async Task<bool> DeleteComponentAsync(Guid id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null) return false;
            _context.Components.Remove(component);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Component>> GetAllComponentsAsync()
        {
            return await _context.Components
                .Include(b => b.Board)
                .ToListAsync();
        }

        public async Task<Component?> GetComponentByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching Component {id}", id);
            return await _context.Components
            .Include(c => c.Board)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> UpdateComponentAsync(Guid id, Component component)
        {
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Component {id} updated successfully", id);
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Concurrency issue updating component {id}", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating component {id}", id);
                return false;
            }
        }
    }
}
