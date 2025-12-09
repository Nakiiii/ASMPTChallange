using Backend.Models;

namespace Backend.Services
{
    public interface IComponentService
    {
        Task<List<Component>> GetAllAsync();
        Task<Component?> GetByIdAsync(Guid id);
        Task<Component> AddAsync(Component component);
        Task<bool> UpdateAsync(Component component);
        Task<bool> DeleteAsync(Guid id);
    }
}
